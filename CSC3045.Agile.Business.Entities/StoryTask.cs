using System.Runtime.Serialization;
using System.Xml.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class StoryTask : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int StoryTaskId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Hours { get; set; }

        [DataMember]
        public bool IsBlocked { get; set; }

        [DataMember]
        public string UserNotes { get; set; }

        //Relationships

        [DataMember]
        public Account Owner { get; set; }

        [DataMember]
        public UserStory UserStory { get; set; }

        [DataMember]
        [XmlIgnore]
        public TaskBurndownPoint TaskBurndownPoint { get; set; }

        [DataMember]
        public CurrentStatus CurrentStatus { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return StoryTaskId; }
            set { StoryTaskId = value; }
        }

        #endregion
    }
}