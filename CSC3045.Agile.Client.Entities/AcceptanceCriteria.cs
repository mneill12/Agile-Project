using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace CSC3045.Agile.Client.Entities
{
    public class AcceptanceCriteria : ObjectBase
    {
        private int _acceptanceCriteriaId;
        private string _scenario;
        private ICollection<Criteria> _criteria;
        private bool _isSatisfied;

        public int AcceptanceCriteriaId
        {
            get
            {
                return _acceptanceCriteriaId;
            }
            set
            {
                if (_acceptanceCriteriaId != value)
                {
                    _acceptanceCriteriaId = value;
                    OnPropertyChanged(() => AcceptanceCriteriaId);
                }
            }
        }

        public string Scenario
        {
            get
            {
                return _scenario;
            }
            set
            {
                if (_scenario != value)
                {
                    _scenario = value;
                    OnPropertyChanged(() => Scenario);
                }
            }
        }

        public ICollection<Criteria> Criteria
        {
            get
            {
                return _criteria;
            }
            set
            {
                if (_criteria != value)
                {
                    _criteria = value;
                    OnPropertyChanged(() => Criteria);
                }
            }
        }

        public Boolean IsSatisfied
        {
            get
            {
                return _isSatisfied;
            }
            set
            {
                if (_isSatisfied != value)
                {
                    _isSatisfied = value;
                    OnPropertyChanged(() => IsSatisfied);
                }
            }
        }

    }
}
