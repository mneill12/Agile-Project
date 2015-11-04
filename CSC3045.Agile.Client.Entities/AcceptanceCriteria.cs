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
        int _acceptanceCriteriaId;
        private string _scenario;
        ISet<String> _criteria;
        bool _isSatisfied;

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

        public String Scenario
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

        public ISet<String> Criteria
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
