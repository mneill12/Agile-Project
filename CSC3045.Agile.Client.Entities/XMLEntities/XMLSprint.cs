using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLSprint : ObjectBase
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
                    OnPropertyChanged(() => SprintId);
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
                    OnPropertyChanged(() => ScrumMaster);
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

        public int SprintNumber
        {
            get { return _SprintNumber; }
            set
            {
                if (_SprintNumber != value)
                {
                    _SprintNumber = value;
                    OnPropertyChanged(() => SprintNumber);
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
                    OnPropertyChanged(() => SprintName);
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
                    OnPropertyChanged(() => StartDate);
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
                    OnPropertyChanged(() => EndDate);
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

        public List<XMLAccount> TeamMembers
        {
            get { return _TeamMembers; }
            set
            {
                if (_TeamMembers != value)
                {
                    _TeamMembers = value;
                    OnPropertyChanged(() => TeamMembers);
                }
            }
        }
    }
}