using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLProject
    {
        private int _ProjectId;
        private XMLAccount _ProjectManager;
        private XMLAccount _ProductOwner;

        private List<XMLAccount> _ScrumMasters;
        private List<XMLAccount> _Developers;

        private string _ProjectName;
        private DateTime _ProjectStartDate;
        private DateTime _ProjectSavedDate;
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
                }
            }
        }

        public DateTime ProjectSavedDate
        {
            get { return _ProjectSavedDate; }
            set
            {
                if (_ProjectSavedDate != value)
                {
                    _ProjectSavedDate = value;
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
                }
            }
        }
    }
}