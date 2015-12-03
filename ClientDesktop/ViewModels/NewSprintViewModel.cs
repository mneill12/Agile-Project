using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using ClientDesktop;
using Prism.Regions;


namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewSprintViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _SprintName;
        private string _SprintStartDate;
        private string _SprintEndDate;

        private int _currentProjectId

        private List<Account> _ProductOwners;
        private List<Account> _ScrumMasters;
        private List<Account> _Developers;

        private readonly IServiceFactory _ServiceFactory;
        private readonly IRegionManager _RegionManager;
        

        [ImportingConstructor]
        public NewSprintViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            CreateSprint = new DelegateCommand<TextBox>(OnCreateSprint);

            _ProductOwners = new List<Account>();

            _ScrumMasters = new List<Account>();

            _Developers = new List<Account>();
        }

        public DelegateCommand<TextBox> CreateSprint { get; private set; }


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
            get { return _currentProjectId; }
            set
            {
                if (_currentProjectId == value) return;
                _currentProjectId = value;
                OnPropertyChanged("ProjectId");
            }
        }

        public string SprintStartDate
        {
            get { return _SprintStartDate; }
            set
            {
                if (_SprintStartDate == value) return;
                _SprintStartDate = value;
                OnPropertyChanged("SprintStart");
            }
        }

        public string SprintEndDate
        {
            get { return _SprintEndDate; }
            set
            {
                if (_SprintEndDate == value) return;
                _SprintEndDate = value;
                OnPropertyChanged("SprintFinish");
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
            GetInitialUsers();

        }

        protected void GetInitialUsers()
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {
                var productOwners = accountClient.GetByUserRole(ViewModelConstants.ProductOwner);
                var scrumMasters = accountClient.GetByUserRole(ViewModelConstants.Scrummaster);
                var developers = accountClient.GetByUserRole(ViewModelConstants.Developer);

                if (null != productOwners && productOwners.Any())
                {
                    ProductOwners.AddRange(productOwners);
                }

                if (scrumMasters != null && scrumMasters.Any())
                {
                    ScrumMasters.AddRange(scrumMasters);
                }

                if (developers != null && developers.Any())
                {
                    Developers.AddRange(developers);
                }
            });
        }

        protected void OnCreateSprint(TextBox textBox)
        {

            if (SprintName != null && SprintStartDate != null && SprintEndDate != null)
            {
                try
                {

                    var _Sprint = new Sprint
                    {
                        SprintName = textBox.Text,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        SprintNumber = int.Parse(textBox.Text)

                    };

                    WithClient(_ServiceFactory.CreateClient<ISprintService>(), sprintClient =>
                    {
                        var mySprint = sprintClient.AddSprint(_Sprint);

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
    }
}
