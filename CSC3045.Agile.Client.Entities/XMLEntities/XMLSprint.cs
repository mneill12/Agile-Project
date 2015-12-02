using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLSprint 
    {
        private XMLBacklog _Backlog;
        private List<XMLBurndown> _Burndowns;
        private DateTime _EndDate;
        private XMLAccount _ScrumMaster;

        private int _SprintId;
        private string _SprintName;
        private int _SprintNumber;
        private DateTime _StartDate;
        private List<XMLAccount> _TeamMembers;

        public int SprintId
        {
            get { return _SprintId; }
            set
            {
                if (_SprintId != value)
                {
                    _SprintId = value;
                   
                }
            }
        }

        public XMLAccount ScrumMaster
        {
            get { return _ScrumMaster; }
            set
            {
                if (_ScrumMaster != value)
                {
                    _ScrumMaster = value;
                    
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

        public int SprintNumber
        {
            get { return _SprintNumber; }
            set
            {
                if (_SprintNumber != value)
                {
                    _SprintNumber = value;
                    
                }
            }
        }

        public string SprintName
        {
            get { return _SprintName; }
            set
            {
                if (_SprintName != value)
                {
                    _SprintName = value;
                    
                }
            }
        }

        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    
                }
            }
        }

        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    
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

        public List<XMLAccount> TeamMembers
        {
            get { return _TeamMembers; }
            set
            {
                if (_TeamMembers != value)
                {
                    _TeamMembers = value;
                    
                }
            }
        }
    }
}