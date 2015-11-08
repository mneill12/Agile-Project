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
        public int StoryStatusId { get; set; }

        [DataMember]
        public string StoryStatusName { get; set; }

        // Many to many declarationa
        public virtual ICollection<UserStory> AssociatedUserStories { get; set; }
        public virtual ICollection<StoryTask> AssociatedStoryTasks { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return StoryStatusId; }
            set { StoryStatusId = value; }
        }

        #endregion
    }
}