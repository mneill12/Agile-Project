using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class Account : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int AccountId { get; set; }
        
        [DataMember]
        public string LoginEmail { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public ICollection<UserRole> UserRoles { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return AccountId; }
            set { AccountId = value; }
        }

        #endregion
    }
}