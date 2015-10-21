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
        int _ProjectId;
        int _ScrumMasterId;
        int _BacklogId;
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

        public int BacklogId
        {
            get
            {
                return _BacklogId;
            }
            set
            {
                if (_BacklogId != value)
                {
                    _BacklogId = value;
                    OnPropertyChanged(() => BacklogId);
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

        class SprintValidator : AbstractValidator<Sprint>
        {
            public SprintValidator()
            {
                RuleFor(obj => obj.ProjectId).NotEmpty();
                RuleFor(obj => obj.BacklogId).NotEmpty();
                RuleFor(obj => obj.SprintNumber).GreaterThanOrEqualTo(0);
                RuleFor(obj => obj.SprintName).NotEmpty();
                RuleFor(obj => obj.StartDate).NotNull();
                RuleFor(obj => obj.EndDate).NotNull();
            }
        }

        protected override IValidator GetValidator()
        {
            return new SprintValidator();
        }
    }
}
