using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class CurrentStatus : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int CurrentStatusId { get; set; }

        [DataMember]
        public string CurrentStatusName { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return CurrentStatusId; }
            set { CurrentStatusId = value; }
        }

        #endregion
    }
}