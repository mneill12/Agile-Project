using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using ClientDesktop.Views;
using Core.Common.Core;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Entities;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TopBarViewModel : ViewModelBase
    {
        #region TopBarBindings

        private string _Email;
        private string _FirstName;
        private string _LastName;
        private bool _IsLoggedIn;
        private IList<UserRole> _UserRoles;

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (_Email == value) return;
                _Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                if (_FirstName == value) return;
                _FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                if (LastName == value) return;
                _LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return _IsLoggedIn;
            }
            set
            {
                if (_IsLoggedIn == value) return;
                _IsLoggedIn = value;
                OnPropertyChanged("IsLoggedIn");
            }
        }

        public IList<UserRole> UserRoles
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

        public DelegateCommand<bool> UpdateLoginStatusCommand { get; set; }
        public DelegateCommand<object> LogoutCommand { get; set; }
        public DelegateCommand<object> EditAccountCommand { get; set; }

        #endregion

        private readonly IRegionManager _RegionManager;

        [ImportingConstructor]
        public TopBarViewModel(IRegionManager regionManager)
        {
            _RegionManager = regionManager;

            UpdateLoginStatusCommand = new DelegateCommand<bool>(UpdateLoginStatus);
            LogoutCommand = new DelegateCommand<object>(Logout, CanLogout);
            EditAccountCommand = new DelegateCommand<object>(EditAccount);

            // Links the Update Login Status Command to be accessed globally
            GlobalCommands.IsLoggedIn.RegisterCommand(UpdateLoginStatusCommand);
        }

        private void UpdateLoginStatus(bool isLoggedIn)
        {
            IsLoggedIn = isLoggedIn;
            UserRoles = GlobalCommands.MyAccount.UserRoles;
            Email = GlobalCommands.MyAccount.LoginEmail;
            FirstName = GlobalCommands.MyAccount.FirstName;
            LastName = GlobalCommands.MyAccount.LastName;
        }

        // Resets global properties and the content region
        private void Logout(object parameter)
        {
            //TODO: Move principal permissions here
            GlobalCommands.MyAccount = null;
            IsLoggedIn = false;

            List<object> allViews = new List<object>(_RegionManager.Regions[RegionNames.Content].Views);

            for (var i = 0; i < allViews.Count; i++)
            {
                _RegionManager.Regions[RegionNames.Content].Remove(allViews[i]);
            }

            _RegionManager.Regions[RegionNames.Content].Add(ServiceLocator.Current.GetInstance<LoginRegisterView>());
        }

        private bool CanLogout(object parameter)
        {
            return (GlobalCommands.MyAccount != null);
        }

        private void EditAccount(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(EditAccountView).FullName);
        }
    }
}