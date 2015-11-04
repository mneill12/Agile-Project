using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

// DTO For Sprints
namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class Sprint : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public int SprintId { get; set; }

        [DataMember]
        public Project Project { get; set; }

        [DataMember]
        public int ScrumMasterId { get; set; }

        [DataMember]
        public Backlog Backlog { get; set; }

        [DataMember]
        public int SprintNumber { get; set; }

        [DataMember]
        public String SprintName { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        #region IIdentifiableEntity members

        int IAccountOwnedEntity.OwnerAccountId
        {
            get { return ScrumMasterId; }
        }

        #endregion

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return SprintId; }
            set { SprintId = value; }
        }

        #endregion
    }
}
