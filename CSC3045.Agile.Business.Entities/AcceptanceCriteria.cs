using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class AcceptanceCriteria : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int AcceptanceCriteriaId { get; set; }

        [DataMember]
        public string Scenario { get; set; }

        [DataMember]
        public ICollection<Criteria> Criteria { get; set; }

        [DataMember]
        public bool IsSatisfied { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return AcceptanceCriteriaId; }
            set { AcceptanceCriteriaId = value; }
        }

        #endregion
    }
}