using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities.XMLEntities
{
    [DataContract]
    public class XMLSprint : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int SprintId { get; set; }

        [DataMember]
        public XMLAccount ScrumMaster { get; set; }

        [DataMember]
        public XMLBacklog Backlog { get; set; }

        [DataMember]
        public int SprintNumber { get; set; }

        [DataMember]
        public string SprintName { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public List<XMLBurndown> Burndowns { get; set; }

        [DataMember]
        public List<XMLAccount> SprintMembers { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return SprintId; }
            set { SprintId = value; }
        }

        #endregion
    }
}