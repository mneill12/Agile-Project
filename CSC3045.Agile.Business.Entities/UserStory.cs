﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class UserStory : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int UserStoryId { get; set; }

        [DataMember]
        public string StoryNumber { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int StoryPoints { get; set; }

        [DataMember]
        public string UserNotes { get; set; }

        [DataMember]
        public CurrentStatus CurrentStatus { get; set; }

        [DataMember]
        public ICollection<StoryTask> AssociatedTasks { get; set; }

        [DataMember]
        public ICollection<AcceptanceCriteria> AcceptanceCriteria { get; set; }

        public virtual Project Project { get; set; }

        public virtual Sprint Sprint { get; set; }

        // Many-to-many declarations
        public virtual ICollection<PlanningPokerSession> AssociatedPlanningPokersSessions { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return UserStoryId; }
            set { UserStoryId = value; }
        }

        #endregion
    }
}