using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml;
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
        [StringLength(400)]
        [Index(IsUnique = true)]
        public string LoginEmail { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        //Relationships

        //One Account has many roles
        [DataMember]
        public ICollection<UserRole> UserRoles { get; set; }

        //One Account has many skills
        [DataMember]
        public ICollection<Skill> Skills { get; set; }

        // Multi-Many-To-Many Entity Join Table Association

        //Many users can be be on many projects
        public ICollection<Project> UserFor { get; set; }

        //Many developers can be on many projects
        public ICollection<Project> DeveloperFor { get; set; }

        //Many scrum masters can be on many projects
        public ICollection<Project> ScrumMasterFor { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return AccountId; }
            set { AccountId = value; }
        }

        #endregion
    }
}