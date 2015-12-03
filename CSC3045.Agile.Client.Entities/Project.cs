using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class Project : ObjectBase
    {
        private int _ProjectId;
        private string _ProjectName;
        private DateTime _ProjectStartDate;

        //Relationships
        private Account _ProjectManager;
        private Account _ProductOwner;
        private Burndown _Burndown;
        private ICollection<Sprint> _Sprints;
        private ICollection<UserStory> _BacklogStories;

        //Many to many relationships
        private ICollection<Account> _AllUsers;
        private ICollection<Account> _ScrumMasters;
        private ICollection<Account> _Developers;

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

        public ICollection<UserStory> BacklogStories
        {
            get { return _BacklogStories; }
            set
            {
                if (_BacklogStories != value)
                {
                    _BacklogStories = value;
                    OnPropertyChanged(() => BacklogStories);
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

        public Burndown Burndown
        {
            get { return _Burndown; }
            set
            {
                if (_Burndown != value)
                {
                    _Burndown = value;
                    OnPropertyChanged(() => Burndown);
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