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


namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewSprintViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _SprintName;
        private string _SprintStartDate;
        private string _SprintEndDate;

        private List<SprintAvailableTeamInfo> _ProductOwners;
        private List<SprintAvailableTeamInfo> _ScrumMasters;
        private List<SprintAvailableTeamInfo> _AllUsers;

        private readonly IServiceFactory _ServiceFactory;

        public event PropertyChangedEventHandler PropertyChanged;

        [ImportingConstructor]
        public NewSprintViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;

            CreateSprint = new DelegateCommand<TextBox>(OnCreateSprint);

            _ProductOwners = new List<SprintAvailableTeamInfo>();

            _ScrumMasters = new List<SprintAvailableTeamInfo>();

            _AllUsers = new List<SprintAvailableTeamInfo>();
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

        public List<SprintAvailableTeamInfo> ListAllTeamMembers
        {
            get { return _AllUsers; }
            set
            {
                if (_AllUsers == value) return;
                _AllUsers = value;
                OnPropertyChanged("ListAllTeamMembers");
            }
        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;


        protected override void OnViewLoaded()
        {
            GetAvailableTeamMembers();

        }

        protected void GetAvailableTeamMembers()
        {
            WithClient(_ServiceFactory.CreateClient<IAccountService>(), accountClient =>
            {

                var productOwners = accountClient.GetByUserRole(ViewModelConstants.ProductOwner);
                var scrumMasters = accountClient.GetByUserRole(ViewModelConstants.Scrummaster);
                var allUsers = accountClient.GetAllAccounts();

                if (allUsers != null)
                {
                    foreach (var a in allUsers)
                    {
                        _AllUsers.Add(new SprintAvailableTeamInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
                }

                if (productOwners != null)
                {
                    foreach (var a in productOwners)
                    {
                        _ProductOwners.Add(new SprintAvailableTeamInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
                }

                if (scrumMasters != null)
                {
                    foreach (var a in scrumMasters)
                    {
                        _ScrumMasters.Add(new SprintAvailableTeamInfo(a.FirstName, a.LastName, a.LoginEmail));
                    }
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
