using System;
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

        private string _LoginEmail = "jflyn07n@qub.ac.uk";
        private string _RegisterFirstName;
        private string _RegisterLastName;
        private string _RegisterEmail;
        private string _RegisterConfirmEmail;
        private string _Status;
        private bool _RegisterIsDeveloper;
        private bool _RegisterIsScrumMaster;
        private bool _RegisterIsProductOwner;
        private ICollection<UserRole> _UserRoles;

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

        #endregion

        #region Delegate Commands

        private readonly DelegateCommand<PasswordBox> _RegisterAccount;
        private readonly DelegateCommand<PasswordBox> _AccountLogin;
        private readonly DelegateCommand<object> _LogoutCommand;
        private readonly DelegateCommand<TextBox> _XMLFilePath; 

        public DelegateCommand<PasswordBox> RegisterAccount { get { return _RegisterAccount; } }
        public DelegateCommand<PasswordBox> AccountLogin { get { return _AccountLogin; } }
        public DelegateCommand<object> LogoutCommand { get { return _LogoutCommand; } }
        public DelegateCommand<TextBox> XMLFilePath { get { return _XMLFilePath; } }

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
            _LogoutCommand = new DelegateCommand<object>(Logout, CanLogout);
            _XMLFilePath = new DelegateCommand<TextBox>(XMLButtonClick);

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

                    WithClient<IAccountService>(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
                    {
                        GlobalCommands.MyOwnedTasks = accountClient.GetOwnedTasks(GlobalCommands.MyAccount).ToList();
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


        protected void XMLButtonClick(TextBox sender)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
            }
        }

        private void Logout(object parameter)
        {
            var customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                OnPropertyChanged("AuthenticatedUser");
                OnPropertyChanged("IsAuthenticated");
                Status = string.Empty;
            }
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
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
                var hashedPassword = new HashHelper().CalculateHash(passwordBox.Password, _LoginEmail);

                try
                {
                    ;
                    WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
                    {
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
    }
}