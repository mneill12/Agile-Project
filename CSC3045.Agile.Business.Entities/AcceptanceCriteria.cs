using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public String Criteria { get; set; }

        [DataMember]
        public Boolean IsSatisfied { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return AcceptanceCriteriaId; }
            set { AcceptanceCriteriaId = value; }
        }

        #endregion
    }


}
