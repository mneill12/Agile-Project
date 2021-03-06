using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        //TODO: Move these to top bar or a region of its own, not needed as account info is stored globally now

        #region DashboardView Bindings

        private string _FirstName;
        private string _Surname;
        private string _EmailAddress;
        private string _FullName;
        private List<UserRole> _AvailableRoles;
        private List<Skill> _AvailableSkills;
        private List<Project> _AllProjects;
        private Sprint _SelectedSprint;

        private bool _IsProductOwner;
        private bool _IsProjectManager;
        private bool _IsScrumMaster;

        private Project _SelectedProjectTab;

        public bool IsProductOwner
        {
            get { return _IsProductOwner; }
            set
            {
                if (_IsProductOwner == value) return;
                _IsProductOwner = value;
                OnPropertyChanged("IsProductOwner");
            }
        }

        public bool IsProjectManager
        {
            get { return _IsProductOwner; }
            set
            {
                if (_IsProjectManager == value) return;
                _IsProjectManager = value;
                OnPropertyChanged("IsProjectManager");
            }
        }

        public bool IsScrumMaster
        {
            get { return _IsScrumMaster; }
            set
            {
                if (_IsScrumMaster == value) return;
                _IsScrumMaster = value;
                OnPropertyChanged("IsScrumMaster");
            }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName == value) return;
                _FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string Surname
        {
            get { return _Surname; }
            set
            {
                if (_Surname == value) return;
                _Surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public string EmailAddress
        {
            get { return _EmailAddress; }
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

        public Sprint SelectedSprint
        {
            get { return _SelectedSprint; }
            set
            {
                if (_SelectedSprint == value) return;
                if (value != null)
                {
                    _SelectedSprint = value;
                    OnPropertyChanged("SelectedSprint");
                }
            }
        }

        public Project SelectedProjectTab
        {
            get { return _SelectedProjectTab; }
            set
            {
                if (_SelectedProjectTab == value) return;
                if (value != null)
                {
                    _SelectedProjectTab = value;
                    CheckPermissions();
                    OnPropertyChanged("SelectedProjectTab");
                }
            }
        }

        #endregion

        IServiceFactory _ServiceFactory;
        IRegionManager _RegionManager;

        public DelegateCommand<object> CreateProjectCommand { get; set; }
        public DelegateCommand<object> ManageProjectBacklogCommand { get; set; }
        public DelegateCommand<object> ViewSprintCommand { get; set; }
        public DelegateCommand<object> RefreshProjectsCommand { get; set; }
        public DelegateCommand<object> ViewBurndownCommand { get; set; }
        public DelegateCommand<object> CreateNewSprintCommand { get; set; }
        public DelegateCommand<object> SaveProjectXMLCommand { get; set; } 
        public DelegateCommand<object> CreatePlanningPokerCommand { get; set; }
        public DelegateCommand<object> ManageSprintBacklogCommand { get; set; }

        

        private void CreateProject(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof (CreateProjectView).FullName);
        }

        private void RefreshProjects(object parameter)
        {
            UpdateProjectsForAccount();
        }

        private void CheckPermissions()
        {
            IsProductOwner = isProductOwner();
            IsProjectManager = isProjectManager();
            IsScrumMaster = isScrumMaster();
        }

        private bool isProductOwner()
        {
            if (SelectedProjectTab.ProductOwner.AccountId == GlobalCommands.MyAccount.AccountId)
            {
                return true;
            }

            return false;
        }

        private bool isProjectManager()
        {
            if (SelectedProjectTab.ProjectManager.AccountId == GlobalCommands.MyAccount.AccountId)
            {
                return true;
            }

            return false;
        }

        private bool isScrumMaster()
        {
            if (SelectedProjectTab.ScrumMasters.Contains(GlobalCommands.MyAccount))
            {
                return true;
            }

            return false;
        }

        private void ViewSprint(object parameter)
        {
            if (SelectedSprint != null)
            {
                NavigationParameters navigationParameters = new NavigationParameters();
                navigationParameters.Add("sprintId",
                    SelectedSprint.SprintId);

                _RegionManager.RequestNavigate(RegionNames.Content, typeof(SprintView).FullName,
                    navigationParameters);
            }
        }

        private void ManageProjectBacklog(object parameter)
        {
            //Only a project owner can manage the project backlog
            if (GlobalCommands.MyAccount.UserRoles.Select(u => u.UserRoleName).Where(u => u == "Product Owner").Any())
            {
                NavigationParameters navigationParameters = new NavigationParameters();
                navigationParameters.Add("projectId",
                    SelectedProjectTab.ProjectId);

                _RegionManager.RequestNavigate(RegionNames.Content, typeof(ProductBacklogManagementView).FullName,
                    navigationParameters);
            }
        }

        private void ViewBurndown(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof (SprintBurndownChartView).FullName);
        }

        private void NewSprint(object parameter)
        {
            //Only a scrum master can create a new sprint
            if (GlobalCommands.MyAccount.UserRoles.Select(u => u.UserRoleName).Where(u => u == "Scrum Master").Any())
            {
                NavigationParameters navigationParameters = new NavigationParameters();
                navigationParameters.Add("projectId",
                    SelectedProjectTab.ProjectId);

                _RegionManager.RequestNavigate(RegionNames.Content, typeof (NewSprintView).FullName,
                    navigationParameters);
            }
        }

        private void CreatePlanningPokerSession(object parameter)
        {
            //Only a scrum master can create a new planning poker session
            if (GlobalCommands.MyAccount.UserRoles.Select(u => u.UserRoleName).Where(u => u == "Scrum Master").Any())
            {
            }
        }

        private void ManageSprintBacklog(object parameter)
        {
            if (GlobalCommands.MyAccount.UserRoles.Select(u => u.UserRoleName).Where(u => u == "Scrum Master").Any())
            {
                NavigationParameters navigationParameters = new NavigationParameters();
                navigationParameters.Add("projectId",
                    SelectedProjectTab.ProjectId);

                _RegionManager.RequestNavigate(RegionNames.Content, typeof(ManageSprintView).FullName,
                    navigationParameters);
            }
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
            ViewSprintCommand = new DelegateCommand<object>(ViewSprint);
            ViewBurndownCommand = new DelegateCommand<object>(ViewBurndown);
            CreateNewSprintCommand = new DelegateCommand<object>(NewSprint);
            SaveProjectXMLCommand = new DelegateCommand<object>(SaveProjectXML);
            CreatePlanningPokerCommand = new DelegateCommand<object>(CreatePlanningPokerSession);
            ManageSprintBacklogCommand = new DelegateCommand<object>(ManageSprintBacklog);
        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        public void SaveProjectXML(object sender)
        {
            WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
            {
                projectClient.SaveToXML(SelectedProjectTab);
            });

        }

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

        //ToDo: This is to test the sprint project, needs to be changed so sprints are actually utilised
        public void GetAllSprints()
        {
            ICollection<Sprint> allSprints = null;
            try
            {
                WithClient(_ServiceFactory.CreateClient<ISprintService>(), sprintClient =>
                {
                    allSprints = sprintClient.GetAllSprints();
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