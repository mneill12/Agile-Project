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
    public class PlanningPokerStory : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public int PlanningPokerStoryId { get; set; }

        [DataMember]
        public int PlanningPokerSessionId { get; set; }

        [DataMember]
        public int UserStoryId { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return PlanningPokerStoryId; }
            set { PlanningPokerStoryId = value; }
        }

        #endregion

        #region IAccountOwnedEntity members

        int IAccountOwnedEntity.OwnerAccountId
        {
            get { return PlanningPokerSessionId; }
        }

        #endregion
    }
}
