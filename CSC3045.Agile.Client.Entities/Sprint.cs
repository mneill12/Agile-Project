using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace CSC3045.Agile.Client.Entities
{
    public class Sprint : ObjectBase
    {

        int _SprintId;
        Project _Project;
        int _ScrumMasterId;
        Backlog _Backlog;
        int _SprintNumber;
        String _SprintName;
        DateTime _StartDate;
        DateTime _EndDate;

        public int SprintId
        {
            get
            {
                return _SprintId;
            }
            set
            {
                if (_SprintId != value)
                {
                    _SprintId = value;
                    OnPropertyChanged(() => SprintId);
                }
            }
        }

        public Project Project
        {
            get
            {
                return _Project;
            }
            set
            {
                if (_Project != value)
                {
                    _Project = value;
                    OnPropertyChanged(() => Project);
                }
            }
        }

        public int ScrumMasterId
        {
            get
            {
                return _ScrumMasterId;
            }
            set
            {
                if (_ScrumMasterId != value)
                {
                    _ScrumMasterId = value;
                    OnPropertyChanged(() => ScrumMasterId);
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

        public int SprintNumber
        {
            get
            {
                return _SprintNumber;
            }
            set
            {
                if (_SprintNumber != value)
                {
                    _SprintNumber = value;
                    OnPropertyChanged(() => SprintNumber);
                }
            }
        }

        public String SprintName
        {
            get
            {
                return _SprintName;
            }
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
            get
            {
                return _StartDate;
            }
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
            get
            {
                return _EndDate;
            }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged(() => EndDate);
                }
            }
        }
    }
}
