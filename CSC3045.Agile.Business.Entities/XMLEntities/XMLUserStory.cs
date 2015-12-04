using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;
using CSC3045.Agile.Business.Entities.XMLEntities;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class XMLUserStory : EntityBase, IIdentifiableEntity
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
        public CurrentStatus Status { get; set; }

        [DataMember]
        public List<XMLStoryTask> AssociatedTasks { get; set; }

        [DataMember]
        public List<XMLAcceptanceCriteria> AcceptanceCriteria { get; set; }

        // Many-to-many declarations
        [XmlIgnore]
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