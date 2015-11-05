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


namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoginRegisterViewModel : ViewModelBase
    {
        private readonly DelegateCommand<object> _loginCommand;
        private readonly DelegateCommand<object> _logoutCommand;
        private readonly DelegateCommand<object> _showViewCommand;
        private string _username;
        private string _status;
        private Account _autheticatedUser;

        #region Properties
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
        #endregion

        #region Commands
        public DelegateCommand<object> LoginCommand { get { return _loginCommand; } }

        public DelegateCommand<object> LogoutCommand { get { return _logoutCommand; } }

        public DelegateCommand<object> ShowViewCommand { get { return _showViewCommand; } }
        #endregion

        [Import]
        public DashboardViewModel DashboardViewModel { get; private set; }

        // Import service factory so we can have 'stateful' contracts and closes proxies when service methods are finished
        [ImportingConstructor]
        public LoginRegisterViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            _loginCommand = new DelegateCommand<object>(Login, CanLogin);
            _logoutCommand = new DelegateCommand<object>(Logout, CanLogout);
        }

        IServiceFactory _ServiceFactory;


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
            try
            {

                WithClient<IAuthenticationService>(_ServiceFactory.CreateClient<IAuthenticationService>(), AuthenticationClient =>

                {
                    _autheticatedUser = AuthenticationClient.AuthenticateUser(_username, clearTextPassword);

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
