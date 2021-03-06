﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class UserRole : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int UserRoleId { get; set; }

        [DataMember]
        public string UserRoleName { get; set; }

        [XmlIgnore]
        public ICollection<Account> Accounts { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return UserRoleId; }
            set { UserRoleId = value; }
        }

        #endregion
    }
}