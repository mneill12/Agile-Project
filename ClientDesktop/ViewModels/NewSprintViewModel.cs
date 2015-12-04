using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using ClientDesktop;
using ClientDesktop.Views;
using Prism.Regions;


namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewSprintViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _SprintName;
        private DateTime _SprintStart;
        private DateTime _SprintEnd;
        private string _DeveloperSearchText;

        private int _CurrentProjectId;
        private int _SprintNumber;

        private List<Account> _ProductOwners;
        private List<Account> _ScrumMasters;
        private List<Account> _Developers;
        private List<Account> _SelectedDevelopers;

        private readonly IServiceFactory _ServiceFactory;
        private readonly IRegionManager _RegionManager;
        

        [ImportingConstructor]
        public NewSprintViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            CreateSprint = new DelegateCommand<object>(OnCreateSprint);

            NavigationDashboardCommand = new DelegateCommand<object>(NavigateDashboard);

            _ProductOwners = new List<Account>();

            _ScrumMasters = new List<Account>();

            _Developers = new List<Account>();

            _SelectedDevelopers = new List<Account>();

            _SprintStart = DateTime.Today;

            _SprintEnd = DateTime.Today;
        }

        public DelegateCommand<object> CreateSprint { get; private set; }

        public DelegateCommand<object> NavigationDashboardCommand { get; private set; }

        public string SprintName
        {
            get { return _SprintName; }
            set
            {
                if (_SprintName == value) return;
                _SprintName = value;
                OnPropertyChanged("SprintName");
            }
        }

        public int currentProjectId
        {
            get { return _CurrentProjectId; }
            set
            {
                if (_CurrentProjectId == value) return;
                _CurrentProjectId = value;
                OnPropertyChanged("ProjectId");
            }
        }

        public int SprintNumber
        {
            get { return _SprintNumber; }
            set
            {
                if (_SprintNumber == value) return;
                _SprintNumber = value;
                OnPropertyChanged("SprintNumber");
            }
        }


        public string DeveloperSearchText
        {
            get
            {
                return _DeveloperSearchText;
            }
            set
            {
                if (_DeveloperSearchText == value) return;
                _DeveloperSearchText = value;
                OnPropertyChanged("DeveloperSearchText");
            }
        }

        public List<Account> SelectedDevelopers
        {
            get { return _SelectedDevelopers; }
            set
            {
                if (_SelectedDevelopers == value) return;
                _SelectedDevelopers = value;
                OnPropertyChanged("SelectedDevelopers");
            }
        }

        public DateTime SprintStart
        {
            get { return _SprintStart; }
            set
            {
                if (_SprintStart == value) return;
                _SprintStart = value;
                OnPropertyChanged("SprintStart");
            }
        }

        public DateTime SprintEnd
        {
            get { return _SprintEnd; }
            set
            {
                if (_SprintEnd == value) return;
                _SprintEnd = value;
                OnPropertyChanged("SprintEnd");
            }
        }

        public List<Account> ProductOwners
        {
            get { return _ProductOwners; }
            set
            {
                if (_ProductOwners == value) return;
                _ProductOwners = value;
                OnPropertyChanged("ProductOwners");
            }
        }

        public List<Account> Developers
        {
            get { return _Developers; }
            set
            {
                if (_Developers == value) return;
                _Developers = value;
                OnPropertyChanged("Developers");
            }
        }

        public List<Account> ScrumMasters
        {
            get { return _ScrumMasters; }
            set
            {
                if (_ScrumMasters == value) return;
                _ScrumMasters = value;
                OnPropertyChanged("ScrumMasters");
            }
        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var id = (int)navigationContext.Parameters["projectId"];
            currentProjectId = id;
            OnViewLoaded();
        }

        protected override void OnViewLoaded()
        {
            getDevelopers();
            GetInitialUsers();
        }

        private void NavigateDashboard(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(DashboardView).FullName);
        }
        protected void GetInitialUsers()
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                var productOwners = accountClient.GetByUserRole(ViewModelConstants.ProductOwner);
                var scrumMasters = accountClient.GetByUserRole(ViewModelConstants.Scrummaster);

                if (null != productOwners && productOwners.Any())
                {
                    ProductOwners.AddRange(productOwners);
                }

                if (scrumMasters != null && scrumMasters.Any())
                {
                    ScrumMasters.AddRange(scrumMasters);
                }

            });
        }

        protected void OnCreateSprint(object par)
        {

            var values = (object[])par;

            IList selectedDevelopers = values[0] as IList;
            selectedDevelopers = selectedDevelopers.Cast<Account>().ToList();

            if (values != null && SprintName != null && selectedDevelopers.Count > 0 )
            {
                //Get Selected Users then add them
                List<Account> sprintMembers= new List<Account>();

                foreach (Account developer in selectedDevelopers)
                {
                    sprintMembers.Add(developer);
                }
                sprintMembers.AddRange(ScrumMasters);

                Sprint localSprint = new Sprint
                {

                    EndDate = SprintEnd,
                    StartDate = SprintStart,
                    SprintName = SprintName,
                    SprintNumber = SprintNumber,
                    ScrumMaster = ScrumMasters.FirstOrDefault(),
                    SprintMembers = sprintMembers,
                   
                };

                Project project = new Project();

                WithClient(_ServiceFactory.CreateClient<ISprintService>(), sprintClient =>
                {
                    sprintClient.AddSprint(localSprint);
                });

                //Navigate back to dashbaord
                _RegionManager.RequestNavigate(RegionNames.Content, typeof(DashboardView).FullName);

            }
            else
            {
                MessageBox.Show("Can't create sprint. Enter all required data");
            }
        
        }

        public class SprintAvailableTeamInfo
        {
            public SprintAvailableTeamInfo(string fname, string sname, string email)
            {
                FirstName = fname;
                Surname = sname;
                EmailAddress = email;
            }

            public string FirstName { get; set; }
            public string Surname { get; set; }
            public string EmailAddress { get; set; }

        }


        protected void getDevelopers()
        {

            WithClient(_ServiceFactory.CreateClient<IProjectService>(), ProjectClient =>
            {
                var projectDevelopers = ProjectClient.GetProjectInfo(_CurrentProjectId).Developers;

                Developers.Clear();

                if (null != projectDevelopers && projectDevelopers.Count != 0)
                {
                    Developers.AddRange(projectDevelopers);
                }

            });
        }
    }
}
