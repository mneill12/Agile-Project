﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class CurrentStatus : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int StoryStatusId { get; set; }

        [DataMember]
        public String StoryStatusName { get; set; }

        // Many to many declarationa
        public virtual ISet<UserStory> AssociatedUserStories { get; set; }
        public virtual ISet<StoryTask> AssociatedStoryTasks { get; set; } 

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return StoryStatusId; }
            set { StoryStatusId = value; }
        }

        #endregion
    }


}
