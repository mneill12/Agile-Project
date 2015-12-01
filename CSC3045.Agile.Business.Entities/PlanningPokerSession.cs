using System;
using System.Collections.Generic;
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
        public ICollection<Account> InvitedAccountSet { get; set; }

        [DataMember]
        public ICollection<UserStory> UserStories { get; set; }

        [DataMember]
        public ICollection<UserStory> CompletedUserStories { get; set; }

        [DataMember]
        public Account ScrumMaster { get; set; }

        [DataMember]
        public ICollection<ChatMessage> Messages { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return PlanningPokerSessionId; }
            set { PlanningPokerSessionId = value; }
        }

        #endregion
    }
}