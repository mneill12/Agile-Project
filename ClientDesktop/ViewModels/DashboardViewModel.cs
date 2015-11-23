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
        private List<ProjectViewModel> _ProjectViewModels;
        private ProjectViewModel _CurrentProjectViewModel;

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

        public List<ProjectViewModel> ProjectViewModels
        {
            get { return _ProjectViewModels; }
            set
            {
                if (_ProjectViewModels == value) return;
                _ProjectViewModels = value;
                OnPropertyChanged("ProjectViewModels");
            }
        }

        public ProjectViewModel CurrentProjectViewModel
        {
            get { return _CurrentProjectViewModel; }
            set
            {
                if (_CurrentProjectViewModel == value) return;
                _CurrentProjectViewModel = value;
                OnPropertyChanged("CurrentProjectViewModel");
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

            UpdateProjectsForAccount();
        }

        protected void UpdateProjectsForAccount()
        {
            ICollection<Project> allProjects = null;

            try
            {
                WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
                {
                     allProjects = projectClient.GetProjectsForAccount(GlobalCommands.MyAccount.AccountId);
                });
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                    ErrorOccured(this, new ErrorMessageEventArgs(ex.Message));
            }

            if (allProjects != null)
            {
                List<Project> projects = new List<Project>();

                projects.AddRange(allProjects);

                foreach (var project in projects)
                {
                    ProjectViewModels.Add(new ProjectViewModel(_ServiceFactory, _RegionManager, project));
                }
            }
        }
    }
}