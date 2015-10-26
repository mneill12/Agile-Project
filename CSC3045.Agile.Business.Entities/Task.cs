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
    public class Task : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int TaskId { get; set; }

        [DataMember]
        public int UserStoryId { get; set; }

        [DataMember]
        public Account Owner { get; set; }

        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public int Hours { get; set; }

        [DataMember]
        public StoryStatus Status { get; set; }

        [DataMember]
        public Boolean Blocked { get; set; }

        [DataMember]
        public String UserNotea { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return TaskId; }
            set { TaskId = value; }
        }

        #endregion
    }
}