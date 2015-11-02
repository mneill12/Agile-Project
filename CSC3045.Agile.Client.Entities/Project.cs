using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace CSC3045.Agile.Client.Entities
{
    public class Project : ObjectBase
    {
        private int _ProjectId;
        private Backlog _Backlog;
        private Account _ProjectManager;
        private Account _ProductOwner;
        private string _ProjectName;
        private DateTime _ProjectDeadline;
        private ISet<Sprint> _Sprints; 

        public int ProjectId
        {
            get
            {
                return _ProjectId;
            }
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
            get
            {
                return _Backlog;
            }
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
            get
            {
                return _ProjectManager;
            }
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
            get
            {
                return _ProductOwner;
            }
            set
            {
                if (_ProductOwner != value)
                {
                    _ProductOwner = value;
                    OnPropertyChanged(() => ProductOwner);
                }
            }
        }

        public String ProjectName
        {
            get
            {
                return _ProjectName;
            }
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
            get
            {
                return _ProjectDeadline;
            }
            set
            {
                if (_ProjectDeadline != value)
                {
                    _ProjectDeadline = value;
                    OnPropertyChanged(() => ProjectDeadline);
                }
            }
        }

        public ISet<Sprint> Sprints
        {
            get
            {
                return _Sprints;
            }
            set
            {
                if (_Sprints != value)
                {
                    _Sprints = value;
                    OnPropertyChanged(() => Sprints);
                }
            }
        }
    }
}
