using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities.XMLEntities
{
    [DataContract]
    public class XMLBacklog : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int BacklogId { get; set; }

        [DataMember]
        public List<XMLUserStory> UserStories { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return BacklogId; }
            set { BacklogId = value; }
        }

        #endregion
    }
}