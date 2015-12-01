using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class TaskBurndownPoint: EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int TaskBurndownPointId { get; set; }

        [DataMember]
        public int StoryTaskId { get; set; }

        [DataMember]
        public Dictionary<DateTime, int> HoursRemaining {get; set;}

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return TaskBurndownPointId; }
            set { TaskBurndownPointId = value; }
        }

        #endregion
    }
}
