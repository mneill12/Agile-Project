using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Security;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using Core.Common.Utils;
using CSC3045.Agile.Client.CustomPrinciples;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using System.ServiceModel;
using Core.Common;


namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoginRegisterViewModel : ViewModelBase
    {
        #region Binding Properties

        private string _username;
        private string _status;
        private Account _autheticatedUser;

        private string _LoginEmail;
        private string _RegisterFirstName;
        private string _RegisterLastName;
        private string _RegisterEmail;
        private string _RegisterConfirmEmail;


        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged("Username"); }
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

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
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

        #endregion

        #region Delegate Commands

        private readonly DelegateCommand<object> _LoginCommand;
        private readonly DelegateCommand<object> _LogoutCommand;
        private readonly DelegateCommand<object> _ShowViewCommand;
        private readonly DelegateCommand<PasswordBox> _RegisterAccount;
        private readonly DelegateCommand<PasswordBox> _AccountLogin;

        public DelegateCommand<object> LoginCommand { get { return _LoginCommand; } }

        public DelegateCommand<object> LogoutCommand { get { return _LogoutCommand; } }

        public DelegateCommand<object> ShowViewCommand { get { return _ShowViewCommand; } }

        public DelegateCommand<PasswordBox> RegisterAccount { get { return _RegisterAccount; } }
        public DelegateCommand<PasswordBox> AccountLogin { get { return _AccountLogin; } }

        #endregion

        [Import]
        public DashboardViewModel DashboardViewModel { get; private set; }

        // Import service factory so we can have 'stateful' contracts and closes proxies when service methods are finished
        [ImportingConstructor]
        public LoginRegisterViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;

            _LoginCommand = new DelegateCommand<object>(Login, CanLogin);
            _LogoutCommand = new DelegateCommand<object>(Logout, CanLogout);
            _RegisterAccount = new DelegateCommand<PasswordBox>(OnRegisterAccount);
            _AccountLogin = new DelegateCommand<PasswordBox>(OnAccountLogin);
        }

        IServiceFactory _ServiceFactory;

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;


        public override string ViewTitle
        {
            get { return "Login/Register"; }
        }

        protected override void OnViewLoaded()
        {

        }

        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            HashHelper hashHelper = new HashHelper();
            string hashedPassword = hashHelper.CalculateHash(clearTextPassword, _username);

            try
            {
                WithClient<IAuthenticationService>(_ServiceFactory.CreateClient<IAuthenticationService>(), AuthenticationClient =>
                {
                    _autheticatedUser = AuthenticationClient.AuthenticateUser(_username, hashedPassword);

                });

                if(_autheticatedUser == null)
                {
                    throw new UnauthorizedAccessException();
                }

                //Get the current principal object
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                //Authenticate the user by setting the custome principle
                UserRole[] userRoles = new UserRole[_autheticatedUser.UserRoles.Count];
                _autheticatedUser.UserRoles.CopyTo(userRoles, 0);
                customPrincipal.Identity = new CustomIdentity(_autheticatedUser.LoginEmail, userRoles);

                //Update UI
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                Username = string.Empty; //reset
                passwordBox.Password = string.Empty; //reset
                Status = string.Empty;
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login failed! Please provide some valid credentials.";
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
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
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
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

        protected void OnAccountLogin(PasswordBox passwordBox)
        {
            if (_LoginEmail != null && passwordBox.Password != null)
            {
                try
                {
                    WithClient<IAccountService>(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
                    {
                        Account myAccount = accountClient.GetAccountInfoWithPasswordAndUserRoles(_LoginEmail, passwordBox.Password);

                        if (myAccount != null)
                        {
                            //ObjectBase.Container.GetExportedValue<DashboardViewModel>();
                        }
                    });
                }
                catch (FaultException ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
                catch (Exception ex)
                {
                    if (ErrorOccured != null)
                        ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
                }
            }

        }

        protected void OnRegisterAccount(PasswordBox passwordBox)
        {
            if (_RegisterEmail != null && passwordBox.Password != null && _RegisterFirstName != null && _RegisterLastName != null)
            {
                try
                {
                    Account _Account = new Account()
                    {
                        LoginEmail = _RegisterEmail,
                        Password = passwordBox.Password,
                        FirstName = _RegisterFirstName,
                        LastName = _RegisterLastName
                    };

                    WithClient<IAccountService>(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
                    {
                        Account myAccount = accountClient.RegisterAccount(_Account);

                        if (myAccount != null)
                        {
                            //ObjectBase.Container.GetExportedValue<DashboardViewModel>();
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
                    ErrorOccured(this,
                        new ErrorMessageEventArgs("Please complete all fields to register an account"));

            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }
}
