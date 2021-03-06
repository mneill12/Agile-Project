﻿using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
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
        public DateTime BurndownPointDate { get; set; }

        [DataMember]
        public int PointsRemaining { get; set; }

        [DataMember]
        public int HoursRemaining { get; set; }

        [DataMember]
        [XmlIgnore]
        public Burndown Burndown { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return BurndownPointId; }
            set { BurndownPointId = value; }
        }

        #endregion
    }
}