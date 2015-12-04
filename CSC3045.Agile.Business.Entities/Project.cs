using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
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
        public string ProjectName { get; set; }

        [DataMember]
        public DateTime ProjectStartDate { get; set; }

        //Relationships

        [DataMember]
        public Account ProjectManager { get; set; }

        [DataMember]
        public Account ProductOwner { get; set; }

        //One project can have one burndown
        [DataMember]
        public Burndown Burndown { get; set; }

        [DataMember]
        [InverseProperty("SprintFor")]
        public ICollection<Sprint> Sprints { get; set; }

        public ICollection<UserStory> BacklogStories { get; set; }

        //Many to many relationships

        //A project can have many users
        [DataMember]
        [InverseProperty("UserFor")]
        public ICollection<Account> AllUsers { get; set; }

        //A project can have many scrum masters
        [DataMember]
        [InverseProperty("ScrumMasterFor")]
        public ICollection<Account> ScrumMasters { get; set; }

        //A project can have many developers
        [DataMember]
        [InverseProperty("DeveloperFor")]
        public ICollection<Account> Developers { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return ProjectId; }
            set { ProjectId = value; }
        }

        #endregion
    }
}