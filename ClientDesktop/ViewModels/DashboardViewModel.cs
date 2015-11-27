using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ClientDesktop.Views;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardViewModel : ViewModelBase
    {
        //TODO: Move these to top bar or a region of its own, not needed as account info is stored globally
        #region DashboardView Bindings

        private string _FirstName;
        private string _Surname;
        private string _EmailAddress;
        private string _FullName;
        private List<UserRole> _AvailableRoles;
        private List<Project> _AllProjects;
        private int _CurrentProjectId;

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

        public string Surname
        {
            get
            {
                return _Surname;
            }
            set
            {
                if (_Surname == value) return;
                _Surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public string EmailAddress
        {
            get
            {
                return _EmailAddress;
            }
            set
            {
                if (_EmailAddress == value) return;
                _EmailAddress = value;
                OnPropertyChanged("EmailAddress");
            }
        }

        public string FullName
        {
            get { return _FullName; }
            set
            {
                if (_FullName == value) return;
                _FullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public List<UserRole> AvailableRoles
        {
            get { return _AvailableRoles; }
            set
            {
                if (_AvailableRoles == value) return;
                _AvailableRoles = value;
                OnPropertyChanged("AvailableRoles");
            }
        }

        public List<Project> AllProjects
        {
            get { return _AllProjects; }
            set
            {
                if (_AllProjects == value) return;
                _AllProjects = value;
                OnPropertyChanged("AllProjects");
            }
        }

        public int CurrentProjectId
        {
            get { return _CurrentProjectId; }
            set
            {
                if (_CurrentProjectId == value) return;
                _CurrentProjectId = value;
                OnPropertyChanged("CurrentProjectId");
            }
        }

        #endregion

        IServiceFactory _ServiceFactory;
        IRegionManager _RegionManager;

        public DelegateCommand<object> CreateProjectCommand { get; set; }

        private void CreateProject(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(CreateProjectView).FullName);
        }

        [ImportingConstructor]
        public DashboardViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            AllProjects = new List<Project>();

            CreateProjectCommand = new DelegateCommand<object>(CreateProject);
        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        protected override void OnViewLoaded()
        {
            FirstName = GlobalCommands.MyAccount.FirstName;
            Surname = GlobalCommands.MyAccount.LastName;
            EmailAddress = GlobalCommands.MyAccount.LoginEmail;
            FullName = GlobalCommands.MyAccount.FirstName + " " + GlobalCommands.MyAccount.LastName;
            AvailableRoles = GlobalCommands.MyAccount.UserRoles.ToList();
        }

        // Event triggered when view is navigated to
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            UpdateProjectsForAccount();
        }
        
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void UpdateProjectsForAccount()
        {
            ICollection<Project> allProjects = null;

            try
            {
                WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
                {
                     allProjects = projectClient.GetProjectsForAccount(GlobalCommands.MyAccount.AccountId);
                });

                if (allProjects != null)
                {
                    AllProjects.Clear();
                    AllProjects.AddRange(allProjects);

                    if (CurrentProjectId == 0)
                    {
                        CurrentProjectId = AllProjects[0].ProjectId;
                    }

                    // TODO: Set focus on created project after navigation after ProjectViewModel has been finished
                    //if (CurrentProjectId != 0)
                    //{
                    //    ProjectViewModel createdProjectViewModel = ProjectViewModels.Find(p => p.ProjectId == CurrentProjectId);

                    //    if (createdProjectViewModel != null)
                    //    {
                    //        CurrentProjectViewModel = createdProjectViewModel;
                    //    }

                    //    CurrentProjectId = 0;
                    //}
                }
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                    ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
            }
        }
    }
}