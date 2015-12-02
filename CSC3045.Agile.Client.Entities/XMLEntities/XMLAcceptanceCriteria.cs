using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLAcceptanceCriteria : ObjectBase
    {
        private int _acceptanceCriteriaId;
        private List<Criteria> _criteria;
        private bool _isSatisfied;
        private string _scenario;

        public int AcceptanceCriteriaId
        {
            get { return _acceptanceCriteriaId; }
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
            get { return _scenario; }
            set
            {
                if (_scenario != value)
                {
                    _scenario = value;
                    OnPropertyChanged(() => Scenario);
                }
            }
        }

        public List<Criteria> Criteria
        {
            get { return _criteria; }
            set
            {
                if (_criteria != value)
                {
                    _criteria = value;
                    OnPropertyChanged(() => Criteria);
                }
            }
        }

        public bool IsSatisfied
        {
            get { return _isSatisfied; }
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