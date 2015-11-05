﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public String StoryNumber { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public int StoryPoints { get; set; }

        [DataMember]
        public String UserNotes { get; set; }

        [DataMember]
        public CurrentStatus Status { get; set; }

        [DataMember]
        public ISet<StoryTask> AssociatedTasks { get; set; }

        [DataMember]
        public ISet<AcceptanceCriteria> AcceptanceCriteria { get; set; }

        // Many-to-many declarations
        public virtual ISet<PlanningPokerSession> AssociatedPlanningPokersSessions { get; set; } 

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return UserStoryId; }
            set { UserStoryId = value; }
        }

        #endregion
    }
}