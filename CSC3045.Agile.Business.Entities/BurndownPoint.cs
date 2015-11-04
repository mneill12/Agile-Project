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
    public class BurndownPoint : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int BurndownPointId { get; set; }

        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public int SprintId { get; set; }

        [DataMember]
        public DateTime BurndownPointDate { get; set; }

        [DataMember]
        public int PointsRemaining { get; set; }

        [DataMember]
        public int HoursRemaining { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return BurndownPointId; }
            set { BurndownPointId = value; }
        }

        #endregion
    }


}
