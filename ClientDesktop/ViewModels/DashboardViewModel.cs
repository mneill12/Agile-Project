using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Navigation;
using ClientDesktop.Views;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using Microsoft.Practices.ServiceLocation;
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
        private List<Skill> _AvailableSkills;
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

        public List<Skill> AvailableSkills
        {
            get { return _AvailableSkills; }
            set
            {
                if (_AvailableSkills == value) return;
                _AvailableSkills = value;
                OnPropertyChanged("AvailableSkills");
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
        public DelegateCommand<object> ManageProjectBacklogCommand { get; set; } 
        public DelegateCommand<object> RefreshProjectsCommand { get; set; }
        public DelegateCommand<object> ViewBurndownCommand { get; set; }

        private void CreateProject(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(CreateProjectView).FullName);
        }

        private void RefreshProjects(object parameter)
        {
            UpdateProjectsForAccount();
        }

        private void ManageProjectBacklog(object parameter)
        {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("projectId",
                ServiceLocator.Current.GetInstance<DashboardViewModel>().CurrentProjectId);

            _RegionManager.RequestNavigate(RegionNames.Content, typeof (ProductBacklogManagementView).FullName,
                navigationParameters);

        }

        private void ViewBurndown(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(SprintBurndownChartView).FullName);
        }

        [ImportingConstructor]
        public DashboardViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            AllProjects = new List<Project>();

            CreateProjectCommand = new DelegateCommand<object>(CreateProject);
            RefreshProjectsCommand = new DelegateCommand<object>(RefreshProjects);
            ManageProjectBacklogCommand = new DelegateCommand<object>(ManageProjectBacklog);
            ViewBurndownCommand = new DelegateCommand<object>(ViewBurndown);
        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        protected override void OnViewLoaded()
        {
            FirstName = GlobalCommands.MyAccount.FirstName;
            Surname = GlobalCommands.MyAccount.LastName;
            EmailAddress = GlobalCommands.MyAccount.LoginEmail;
            FullName = GlobalCommands.MyAccount.FirstName + " " + GlobalCommands.MyAccount.LastName;

            if (GlobalCommands.MyAccount.UserRoles != null && GlobalCommands.MyAccount.UserRoles.Count > 0)
            {
                AvailableRoles = GlobalCommands.MyAccount.UserRoles.ToList();
            }

            if (GlobalCommands.MyAccount.Skills != null && GlobalCommands.MyAccount.Skills.Count > 0)
            {
                AvailableSkills = GlobalCommands.MyAccount.Skills.ToList();
            }

            UpdateProjectsForAccount();
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
                    List<Project> updatedProjectList = new List<Project>();
                    updatedProjectList.AddRange(allProjects);
                    AllProjects = updatedProjectList;

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