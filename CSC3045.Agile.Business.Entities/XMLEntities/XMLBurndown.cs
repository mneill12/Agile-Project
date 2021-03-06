﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class XMLBurndown : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int BurndownId { get; set; }

        [DataMember]
        public string BurndownName { get; set; }

        [DataMember]
        public List<BurndownPoint> BurndownPoints { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return BurndownId; }
            set { BurndownId = value; }
        }

        #endregion
    }
}