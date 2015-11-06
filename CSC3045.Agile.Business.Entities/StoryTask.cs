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
    public class StoryTask : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int StoryTaskId { get; set; }

        [DataMember]
        public Account Owner { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Hours { get; set; }

        [DataMember]
        public CurrentStatus CurrentStatus { get; set; }

        [DataMember]
        public Boolean IsBlocked { get; set; }

        [DataMember]
        public string UserNotes { get; set; }

       

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return StoryTaskId; }
            set { StoryTaskId = value; }
        }

        #endregion
    }
}