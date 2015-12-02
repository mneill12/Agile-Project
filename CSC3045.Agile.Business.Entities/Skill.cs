using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class Skill : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int SkillId { get; set; }

        [DataMember]
        [StringLength(400)]
        [Index(IsUnique = true)]
        public string SkillName { get; set; }

        [XmlIgnore]
        public ICollection<Account> Accounts { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return SkillId; }
            set { SkillId = value; }
        }

        #endregion
    }
}