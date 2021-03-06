﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows.Controls;
using ClientDesktop.Views;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using Core.Common.Utils;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.CustomPrinciples;
using CSC3045.Agile.Client.Entities;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoginRegisterViewModel : ViewModelBase
    {
        #region LoginRegisterView Bindings

        //TODO: Remove hardcoding from release version
        private string _LoginEmail = "jflyn07n@qub.ac.uk";
        private string _RegisterFirstName;
        private string _RegisterLastName;
        private string _RegisterEmail;
        private string _RegisterConfirmEmail;
        private string _RegisterSkills;
        private string _Status;
        private bool _RegisterIsDeveloper;
        private bool _RegisterIsScrumMaster;
        private bool _RegisterIsProductOwner;
        private ICollection<UserRole> _UserRoles;

        private String _ChosenFilePath;

        public string ChosenFilePath
        {
            get
            {
                return _ChosenFilePath;
            }
            set
            {
                if (_ChosenFilePath == value) return;
                _ChosenFilePath = value;
                OnPropertyChanged("ChosenFilePath");
            }
        }

        public string AuthenticatedUser{
            get
            {
                if (IsAuthenticated)
                    return string.Format("Signed in as {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("Administrators") ? "You are an administrator!"
                              : "You are NOT a member of the administrators group.");

                return "Not authenticated!";
            }
        }

        public string LoginEmail
        {
            get
            {
                return _LoginEmail;
            }
            set
            {
                if (_LoginEmail == value) return;
                _LoginEmail = value;
                OnPropertyChanged("LoginEmail");
            }
        }

        public string RegisterFirstName
        {
            get
            {
                return _RegisterFirstName;
            }
            set
            {
                if (_RegisterFirstName == value) return;
                _RegisterFirstName = value;
                OnPropertyChanged("RegisterFirstName");
            }
        }

        public string RegisterLastName
        {
            get
            {
                return _RegisterLastName;
            }
            set
            {
                if (_RegisterLastName == value) return;
                _RegisterLastName = value;
                OnPropertyChanged("RegisterLastName");
            }
        }

        public string RegisterEmail
        {
            get
            {
                return _RegisterEmail;
            }
            set
            {
                if (_RegisterEmail == value) return;
                _RegisterEmail = value;
                OnPropertyChanged("RegisterEmail");
            }
        }

        public string RegisterConfirmEmail
        {
            get
            {
                return _RegisterConfirmEmail;
            }
            set
            {
                if (_RegisterConfirmEmail == value) return;
                _RegisterConfirmEmail = value;
                OnPropertyChanged("RegisterConfirmEmail");
            }
        }

        public bool RegisterIsDeveloper
        {
            get { return _RegisterIsDeveloper; }
            set
            {
                if (_RegisterIsDeveloper == value) return;
                _RegisterIsDeveloper = value;
                OnPropertyChanged("RegisterIsDeveloper");
            }
        }

        public bool RegisterIsScrumMaster
        {
            get { return _RegisterIsScrumMaster; }
            set
            {
                if (_RegisterIsScrumMaster == value) return;
                _RegisterIsScrumMaster = value;
                OnPropertyChanged("RegisterIsScrumMaster");
            }
        }

        public bool RegisterIsProductOwner
        {
            get { return _RegisterIsProductOwner; }
            set
            {
                if (_RegisterIsProductOwner == value) return;
                _RegisterIsProductOwner = value;
                OnPropertyChanged("RegisterIsProductOwner");
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status == value) return;
                _Status = value;
                OnPropertyChanged("Status");
            }
        }

        public ICollection<UserRole> UserRoles
        {
            get
            {
                return _UserRoles;
            }
            set
            {
                if (_UserRoles == value) return;
                _UserRoles = value;
                OnPropertyChanged("UserRoles");
            }
        }

        public string RegisterSkills
        {
            get
            {
                return _RegisterSkills;
            }
            set
            {
                if (_RegisterSkills == value) return;
                _RegisterSkills = value;
                OnPropertyChanged("RegisterSkills");
            }
        }

        #endregion

        #region Delegate Commands

        private readonly DelegateCommand<PasswordBox> _RegisterAccount;
        private readonly DelegateCommand<PasswordBox> _AccountLogin;
        private readonly DelegateCommand<TextBox> _XMLFilePath;
        private readonly DelegateCommand<TextBox> _UploadXMLDocument; 

        public DelegateCommand<PasswordBox> RegisterAccount { get { return _RegisterAccount; } }
        public DelegateCommand<PasswordBox> AccountLogin { get { return _AccountLogin; } }
        public DelegateCommand<TextBox> XMLFilePath { get { return _XMLFilePath; } }
        public DelegateCommand<TextBox> UploadXMLDocument { get { return _UploadXMLDocument; } }

            #endregion

        [Import]
        public DashboardViewModel DashboardViewModel { get; private set; }

        private IServiceFactory _ServiceFactory;
        private IRegionManager _RegionManager;


        // Import service factory so we can have 'stateful' contracts and closes proxies when service methods are finished
        [ImportingConstructor]
        public LoginRegisterViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;
            _RegisterAccount = new DelegateCommand<PasswordBox>(OnRegisterAccount);
            _AccountLogin = new DelegateCommand<PasswordBox>(OnAccountLogin);
            _XMLFilePath = new DelegateCommand<TextBox>(XMLButtonClick);
            _UploadXMLDocument = new DelegateCommand<TextBox>(UploadXMLDocumentClick);

        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            LoginEmail = "jflyn07n@qub.ac.uk";
        }

        // Never keep login/register view
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }


        protected void OnAccountLogin(PasswordBox passwordBox)
        {
            if (string.IsNullOrEmpty(LoginEmail) || string.IsNullOrEmpty(passwordBox.Password))
            {
                Status = "Please complete all fields to login";
            }
            else
            {
                var hashedPassword = new HashHelper().CalculateHash(passwordBox.Password, LoginEmail);

                try
                {
                    WithClient(_ServiceFactory.CreateClient<IAuthenticationService>(), authenticationClient =>
                    {
                        GlobalCommands.MyAccount = authenticationClient.AuthenticateUser(LoginEmail, hashedPassword);

                        if (GlobalCommands.MyAccount != null)
                        {
                            //Get the current principal object
                            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                            if (customPrincipal == null)
                                throw new ArgumentException(
                                    "The application's default thread principal must be set to a CustomPrincipal object on startup.");

                            //Authenticate the user by setting the custom principles
                            if (GlobalCommands.MyAccount.UserRoles != null)
                            {
                                var userRoles = new UserRole[GlobalCommands.MyAccount.UserRoles.Count];
                                GlobalCommands.MyAccount.UserRoles.CopyTo(userRoles, 0);
                                customPrincipal.Identity = new CustomIdentity(GlobalCommands.MyAccount.LoginEmail,
                                    userRoles);
                            }

                            //Update UI
                            OnPropertyChanged("AuthenticatedUser");
                            OnPropertyChanged("IsAuthenticated");
                            LoginEmail = string.Empty;
                            Status = string.Empty;

                            GlobalCommands.IsLoggedIn.Execute(true);
                            _RegionManager.RequestNavigate(RegionNames.Content, typeof (DashboardView).FullName);
                        }
                        else
                        {
                            throw new UnauthorizedAccessException();
                        }
                    });
                }
                catch (FaultException ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));

                    Status = "Account not found, please check your e-mail and password";
                }
                catch (UnauthorizedAccessException ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));

                    Status = "Login failed! Please provide some valid credentials.";
                }
                catch (Exception ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }


        protected void XMLButtonClick(object sender)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                ChosenFilePath = filename;
            }
        }

        protected void UploadXMLDocumentClick(TextBox sender)
        {
            if (string.IsNullOrEmpty(_ChosenFilePath) || string.IsNullOrEmpty(sender.Text))
            {
                MessageBox.Show("You haven't selected a file!", "XML Upload Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            GlobalCommands.LoadedXMLFilePath = sender.Text;
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(OfflineProjectView).FullName);

        }

        protected void OnRegisterAccount(PasswordBox passwordBox)
        {
            if (string.IsNullOrEmpty(_RegisterEmail) || string.IsNullOrEmpty(passwordBox.Password) ||
                string.IsNullOrEmpty(_RegisterFirstName) || string.IsNullOrEmpty(_RegisterLastName))
            {
                Status = "Please complete all fields to register an account";
            }
            else
            {
                var hashedPassword = new HashHelper().CalculateHash(passwordBox.Password, _RegisterEmail);

                try
                {
                    WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
                    {
                        //Add skills to account
                        var dbSkills = accountClient.GetAllSkills();
                        ISet<string> inputSkills = ParseSkillString();
                        IList<Skill> addedSkills = new List<Skill>();

                        foreach (var inputSkill in inputSkills)
                        {
                            bool skillFound = false;

                            foreach (var skill in dbSkills)
                            {
                                if (skill.SkillName.Equals(inputSkill))
                                {
                                    addedSkills.Add(skill);
                                    skillFound = true;
                                }
                            }

                            if (!skillFound)
                            {
                                addedSkills.Add(new Skill() {SkillName = inputSkill});
                            }
                        }

                        // Get Userroles
                        var allUserRoles = accountClient.GetAllUserRoles();
                        IList<UserRole> selectedUserRoles = new List<UserRole>();

                        if (RegisterIsDeveloper)
                        {
                            selectedUserRoles.Add(allUserRoles[0]);
                        }

                        if (RegisterIsProductOwner)
                        {
                            selectedUserRoles.Add(allUserRoles[1]);
                        }

                        if (RegisterIsScrumMaster)
                        {
                            selectedUserRoles.Add(allUserRoles[2]);
                        }

                        var account = new Account
                        {
                            LoginEmail = _RegisterEmail,
                            Password = hashedPassword,
                            FirstName = _RegisterFirstName,
                            LastName = _RegisterLastName,
                            Skills = addedSkills,
                            UserRoles = selectedUserRoles
                        };

                        GlobalCommands.MyAccount = accountClient.RegisterAccount(account);

                        if (GlobalCommands.MyAccount != null)
                        {
                            GlobalCommands.IsLoggedIn.Execute(true);
                            _RegionManager.RequestNavigate(RegionNames.Content, typeof (DashboardView).FullName);
                        }
                    });
                }
                catch (Exception ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
            }
        }

        /// <summary>
        /// Separates skills that have a string or comma and checks for duplicates to prevent exceptions
        /// </summary>
        /// <returns>A set of unique skill strings</returns>
        private ISet<string> ParseSkillString()
        {
            if (RegisterSkills.Length > 0)
            {
                IList<string> skillNames = RegisterSkills.Split(new string[] { ",", " " },
                StringSplitOptions.RemoveEmptyEntries);

                ISet<string> skillSet = new HashSet<string>();

                foreach (var skill in skillNames)
                {
                    skillSet.Add(skill);
                }

                return skillSet;
            }

            return null;
        }
    }
}