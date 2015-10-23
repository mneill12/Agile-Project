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
    public class Project : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public int BacklogId { get; set; }

        [DataMember]
        public int ProjectManagerId { get; set; }

        [DataMember]
        public int ProductOwnerId { get; set; }

        [DataMember]
        public String ProjectName { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return ProjectId; }
            set { ProjectId = value; }
        }

        #endregion

        #region IAccountOwnedEntity members

        int IAccountOwnedEntity.OwnerAccountId
        {
            get { return ProjectManagerId; }
        }

        #endregion
    }
}