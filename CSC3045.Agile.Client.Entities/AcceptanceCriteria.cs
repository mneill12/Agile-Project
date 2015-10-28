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
        int _AcceptanceCriteriaId;
        string _Criteria;
        bool _IsSatisfied;

        public int AcceptanceCriteriaId
        {
            get
            {
                return _AcceptanceCriteriaId;
            }
            set
            {
                if (_AcceptanceCriteriaId != value)
                {
                    _AcceptanceCriteriaId = value;
                    OnPropertyChanged(() => AcceptanceCriteriaId);
                }
            }
        }

        public String Criteria
        {
            get
            {
                return _Criteria;
            }
            set
            {
                if (_Criteria != value)
                {
                    _Criteria = value;
                    OnPropertyChanged(() => Criteria);
                }
            }
        }

        public Boolean IsSatisfied
        {
            get
            {
                return _IsSatisfied;
            }
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
