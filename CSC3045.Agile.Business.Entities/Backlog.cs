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
    public class Backlog : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int BacklogId { get; set; }

        [DataMember]
        public ISet<UserStory> AssociatedUserStories { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return BacklogId; }
            set { BacklogId = value; }
        }

        #endregion
    }
}