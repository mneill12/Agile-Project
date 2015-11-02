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
        int _ProjectId;
        Backlog _Backlog;
        int _ProjectManagerId;
        int _ProductOwnerId;
        string _ProjectName;
        DateTime _ProjectDeadline;

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

        public int ProjectManagerId
        {
            get
            {
                return _ProjectManagerId;
            }
            set
            {
                if (_ProjectManagerId != value)
                {
                    _ProjectManagerId = value;
                    OnPropertyChanged(() => ProjectManagerId);
                }
            }
        }

        public int ProductOwnerId
        {
            get
            {
                return _ProductOwnerId;
            }
            set
            {
                if (_ProductOwnerId != value)
                {
                    _ProductOwnerId = value;
                    OnPropertyChanged(() => ProductOwnerId);
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

    }
}
