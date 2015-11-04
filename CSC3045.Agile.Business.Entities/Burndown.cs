﻿using System;
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
    public class Burndown : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int BurndownId { get; set; }

        [DataMember]
        public String BurndownName { get; set; }

        [DataMember]
        public ISet<BurndownPoint> BurndownPoints { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return BurndownId; }
            set { BurndownId = value; }
        }

        #endregion
    }
}