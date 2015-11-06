﻿using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Security;
using ClientDesktop.Views;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using Core.Common.Utils;
using CSC3045.Agile.Client.CustomPrinciples;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using System.ServiceModel;
using Microsoft.Practices.ServiceLocation;
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

        #endregion

        #region Delegate Commands

        private readonly DelegateCommand<PasswordBox> _RegisterAccount;
        private readonly DelegateCommand<PasswordBox> _AccountLogin;
        private readonly DelegateCommand<object> _LogoutCommand;

        public DelegateCommand<PasswordBox> RegisterAccount { get { return _RegisterAccount; } }
        public DelegateCommand<PasswordBox> AccountLogin { get { return _AccountLogin; } }
        public DelegateCommand<object> LogoutCommand { get { return _LogoutCommand; } }

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

        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        public override string ViewTitle
        {
            get { return "Login/Register"; }
        }

        protected override void OnViewLoaded()
        {

        }

        protected void OnAccountLogin(PasswordBox passwordBox)
        {
            if (_LoginEmail != null && passwordBox.Password != null)
            {
                string hashedPassword = new HashHelper().CalculateHash(passwordBox.Password, _LoginEmail);

                try
                {
                    WithClient<IAuthenticationService>(_ServiceFactory.CreateClient<IAuthenticationService>(), authenticationClient =>
                    {
                        GlobalCommands.MyAccount = authenticationClient.AuthenticateUser(_LoginEmail, hashedPassword);

                        if (GlobalCommands.MyAccount != null)
                        {
                            //Get the current principal object
                            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                            if (customPrincipal == null)
                                throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                            //Authenticate the user by setting the custom principles
                            if (GlobalCommands.MyAccount.UserRoles != null)
                            {
                                UserRole[] userRoles = new UserRole[GlobalCommands.MyAccount.UserRoles.Count];
                                GlobalCommands.MyAccount.UserRoles.CopyTo(userRoles, 0);
                                customPrincipal.Identity = new CustomIdentity(GlobalCommands.MyAccount.LoginEmail, userRoles);
                            }

                            //Update UI
                            OnPropertyChanged("AuthenticatedUser");
                            OnPropertyChanged("IsAuthenticated");
                            _LoginEmail = string.Empty;
                            Status = string.Empty;

                            GlobalCommands.IsLoggedIn.Execute(true);
                            _RegionManager.RequestNavigate("MainRegion", "DashboardView");
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

        private void Logout(object parameter)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
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

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }

        protected void OnRegisterAccount(PasswordBox passwordBox)
        {
            if (_RegisterEmail != null && passwordBox.Password != null && _RegisterFirstName != null && _RegisterLastName != null)
            {
                string hashedPassword = new HashHelper().CalculateHash(passwordBox.Password, _LoginEmail);

                try
                {
                    Account _Account = new Account()
                    {
                        LoginEmail = _RegisterEmail,
                        Password = hashedPassword,
                        FirstName = _RegisterFirstName,
                        LastName = _RegisterLastName
                    };
                        
                    WithClient<IAccountService>(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
                    {
                        GlobalCommands.MyAccount = accountClient.RegisterAccount(_Account);

                        if (GlobalCommands.MyAccount != null)
                        {
                            _RegionManager.RequestNavigate("MainRegion", "DashboardView");
                        }
                    });
                }
                catch (Exception ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
            }
            else
            {
                if (ErrorOccured != null)
                    ErrorOccured(this, new ErrorMessageEventArgs("Please complete all fields to register an account"));

            }
        }
    }
}
