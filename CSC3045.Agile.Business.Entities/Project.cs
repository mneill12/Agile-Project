using System;
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
    public class Project : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public Backlog Backlog { get; set; }

        [DataMember]
        public Account ProjectManager { get; set; }

        [DataMember]
        public Account ProductOwner { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public DateTime ProjectDeadline { get; set; }

        [DataMember]
        public ICollection<Sprint> Sprints { get; set; } 

        [DataMember]
        public ICollection<Burndown> Burndowns { get; set; }

        [DataMember]
        public ICollection<Account> ProjectMembers { get; set; } 

        [DataMember]
        public ICollection<Account> AssociatedUsers { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return ProjectId; }
            set { ProjectId = value; }
        }

        #endregion

    }
}
