using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TopBarViewModels : ViewModelBase
    {
        #region TopBarBindings

        private string _Email;
        private string _FirstName;
        private string _LastName;
        private bool _IsLoggedIn;
        private ObservableCollection<StoryTask> _OwnedTasks; 

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

        public ObservableCollection<StoryTask> OwnedTasks
        {
            get
            {
                return _OwnedTasks;
            }
            set
            {
                if (_OwnedTasks == value) return;
                _OwnedTasks = value;
                OnPropertyChanged("OwnedTasks");
            }
        }

        #endregion

        IServiceFactory _ServiceFactory;

        #region Delegate Commands

        public DelegateCommand<bool> UpdateLoggedIn { get; set; }  

        #endregion

        public TopBarViewModels()
        {
            UpdateLoggedIn = new DelegateCommand<bool>(UpdateLoginStatus);

            GlobalCommands.IsLoggedIn.RegisterCommand(UpdateLoggedIn);
        }

        private void UpdateLoginStatus(bool isLoggedIn)
        {
            IsLoggedIn = isLoggedIn;
            Email = GlobalCommands.MyAccount.LoginEmail;
            FirstName = GlobalCommands.MyAccount.FirstName;
            LastName = GlobalCommands.MyAccount.LastName;
            if (null != GlobalCommands.MyOwnedTasks)
            {
                AddRange(OwnedTasks, GlobalCommands.MyOwnedTasks);
            }
        }

        public static void AddRange<T>(ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}