using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoginRegisterViewModel : ViewModelBase
    {
        // Import service factory so we can have 'stateful' contracts and closes proxies when service methods are finished
        [ImportingConstructor]
        public LoginRegisterViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;

            RegisterAccount = new DelegateCommand<PasswordBox>(OnRegisterAccount);
            AccountLogin = new DelegateCommand<PasswordBox>(OnAccountLogin);

            CurrentViewModel = this;
        }

        IServiceFactory _ServiceFactory;

        public DelegateCommand<PasswordBox> RegisterAccount { get; private set; }
        public DelegateCommand<PasswordBox> AccountLogin { get; private set; }

        #region LoginRegisterView Bindings

        private string _LoginEmail;

        private string _RegisterFirstName;
        private string _RegisterLastName;
        private string _RegisterEmail;
        private string _RegisterConfirmEmail;

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

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;
        
        public override string ViewTitle
        {
            get { return "Login/Register"; }
        }

        // This gets hit every time the page is loaded while the constructor only gets loaded initially, use for getting up-to-data from the database
        protected override void OnViewLoaded()
        {

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
                            CurrentViewModel = ObjectBase.Container.GetExportedValue<DashboardViewModel>();
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
            if ( _RegisterEmail != null && passwordBox.Password != null && _RegisterFirstName != null && _RegisterLastName != null)
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
                                CurrentViewModel = ObjectBase.Container.GetExportedValue<DashboardViewModel>();
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

    }
}
