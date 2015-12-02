using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLProject : ObjectBase
    {
        private int _ProjectId;
        private XMLAccount _ProjectManager;
        private XMLAccount _ProductOwner;

        private List<XMLAccount> _ScrumMasters;
        private List<XMLAccount> _Developers;

        private string _ProjectName;
        private DateTime _ProjectStartDate;
        private XMLBacklog _Backlog;
        private List<XMLSprint> _Sprints;
        private List<XMLBurndown> _Burndowns;

        private List<XMLAccount> _AllUsers;

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

        public XMLAccount ProjectManager
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

        public XMLAccount ProductOwner
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

        public List<XMLAccount> ScrumMasters
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

        public List<XMLAccount> Developers
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

        public XMLBacklog Backlog
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

        public List<XMLSprint> Sprints
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

        public List<XMLBurndown> Burndowns
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

        public List<XMLAccount> AllUsers
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