using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities.XMLEntities
{
    [DataContract]
    public class XMLAccount : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int AccountId { get; set; }
        
        [DataMember]
        [StringLength(400)]
        [Index(IsUnique = true)]
        public string LoginEmail { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public List<UserRole> UserRoles { get; set; }

        [DataMember]
        public List<Skill> Skills { get; set; }

    

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return AccountId; }
            set { AccountId = value; }
        }

        #endregion
    }
}