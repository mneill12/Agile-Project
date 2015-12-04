using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class AcceptanceCriteria : ObjectBase
    {
        private int _AcceptanceCriteriaId;
        private bool _IsSatisfied;
        private string _Scenario;

        //Relationships
        private ICollection<Criteria> _Criteria;

        public int AcceptanceCriteriaId
        {
            get { return _AcceptanceCriteriaId; }
            set
            {
                if (_AcceptanceCriteriaId != value)
                {
                    _AcceptanceCriteriaId = value;
                    OnPropertyChanged(() => AcceptanceCriteriaId);
                }
            }
        }

        public string Scenario
        {
            get { return _Scenario; }
            set
            {
                if (_Scenario != value)
                {
                    _Scenario = value;
                    OnPropertyChanged(() => Scenario);
                }
            }
        }

        public ICollection<Criteria> Criteria
        {
            get { return _Criteria; }
            set
            {
                if (_Criteria != value)
                {
                    _Criteria = value;
                    OnPropertyChanged(() => Criteria);
                }
            }
        }

        public bool IsSatisfied
        {
            get { return _IsSatisfied; }
            set
            {
                if (_IsSatisfied != value)
                {
                    _IsSatisfied = value;
                    OnPropertyChanged(() => IsSatisfied);
                }
            }
        }
    }
}