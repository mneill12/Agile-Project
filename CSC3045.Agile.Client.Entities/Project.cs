using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class Project : ObjectBase
    {
        private Backlog _Backlog;
        private ICollection<Burndown> _Burndowns;
        private Account _ProductOwner;
        private DateTime _ProjectDeadline;
        private int _ProjectId;
        private Account _ProjectManager;
        private ICollection<Account> _ProjectMembers;
        private string _ProjectName;
        private ICollection<Sprint> _Sprints;

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

        public DateTime ProjectDeadline
        {
            get { return _ProjectDeadline; }
            set
            {
                if (_ProjectDeadline != value)
                {
                    _ProjectDeadline = value;
                    OnPropertyChanged(() => ProjectDeadline);
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

        public ICollection<Account> ProjectMembers
        {
            get { return _ProjectMembers; }
            set
            {
                if (_ProjectMembers != value)
                {
                    _ProjectMembers = value;
                    OnPropertyChanged(() => ProjectMembers);
                }
            }
        }
    }
}