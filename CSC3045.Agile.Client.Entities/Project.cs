using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class Project : ObjectBase
    {
        private int _ProjectId;
        private Account _ProjectManager;
        private Account _ProductOwner;

        private ICollection<Account> _ScrumMasters;
        private ICollection<Account> _Developers;

        private string _ProjectName;
        private DateTime _ProjectStartDate;
        private Backlog _Backlog;
        private ICollection<Sprint> _Sprints;
        private ICollection<Burndown> _Burndowns;

        private ICollection<Account> _AllUsers;

        public int ProjectId
        {
            get { return _ProjectId; }
            set
            {
                if (_ProjectId != value)
                {
                    _ProjectId = value;
                    OnPropertyChanged(() => ProjectId);
                }
            }
        }

        public Account ProjectManager
        {
            get { return _ProjectManager; }
            set
            {
                if (_ProjectManager != value)
                {
                    _ProjectManager = value;
                    OnPropertyChanged(() => ProjectManager);
                }
            }
        }

        public Account ProductOwner
        {
            get { return _ProductOwner; }
            set
            {
                if (_ProductOwner != value)
                {
                    _ProductOwner = value;
                    OnPropertyChanged(() => ProductOwner);
                }
            }
        }

        public ICollection<Account> ScrumMasters
        {
            get { return _ScrumMasters; }
            set
            {
                if (_ScrumMasters != value)
                {
                    _ScrumMasters = value;
                    OnPropertyChanged(() => ScrumMasters);
                }
            }
        }

        public ICollection<Account> Developers
        {
            get { return _Developers; }
            set
            {
                if(_Developers != value)
                {
                    _Developers = value;
                    OnPropertyChanged(() => Developers);
                }
            }
        }

        public string ProjectName
        {
            get { return _ProjectName; }
            set
            {
                if (_ProjectName != value)
                {
                    _ProjectName = value;
                    OnPropertyChanged(() => ProjectName);
                }
            }
        }

        public DateTime ProjectStartDate
        {
            get { return _ProjectStartDate; }
            set
            {
                if (_ProjectStartDate != value)
                {
                    _ProjectStartDate = value;
                    OnPropertyChanged(() => ProjectStartDate);
                }
            }
        }

        public Backlog Backlog
        {
            get { return _Backlog; }
            set
            {
                if (_Backlog != value)
                {
                    _Backlog = value;
                    OnPropertyChanged(() => Backlog);
                }
            }
        }

        public ICollection<Sprint> Sprints
        {
            get { return _Sprints; }
            set
            {
                if (_Sprints != value)
                {
                    _Sprints = value;
                    OnPropertyChanged(() => Sprints);
                }
            }
        }

        public ICollection<Burndown> Burndowns
        {
            get { return _Burndowns; }
            set
            {
                if (_Burndowns != value)
                {
                    _Burndowns = value;
                    OnPropertyChanged(() => Burndowns);
                }
            }
        }

        public ICollection<Account> AllUsers
        {
            get { return _AllUsers; }
            set
            {
                if (_AllUsers != value)
                {
                    _AllUsers = value;
                    OnPropertyChanged(() => AllUsers);
                }
            }
        }
    }
}