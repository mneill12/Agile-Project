﻿using System;
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

        private int _SprintId;
        private Account _ScrumMaster;
        private Backlog _Backlog;
        private int _SprintNumber;
        private String _SprintName;
        private DateTime _StartDate;
        private DateTime _EndDate;

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

        public Account ScrumMaster
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
