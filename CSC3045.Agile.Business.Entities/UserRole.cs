using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
