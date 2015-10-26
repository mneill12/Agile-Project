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
    public class PlanningPokerSession : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int PlanningPokerSessionId { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public HashSet<int> InvitedAccountIdSet { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return PlanningPokerSessionId; }
            set { PlanningPokerSessionId = value; }
        }

        #endregion

    }
}
