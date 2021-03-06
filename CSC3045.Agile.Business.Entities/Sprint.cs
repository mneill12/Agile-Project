﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

// DTO For Sprints

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class Sprint : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int SprintId { get; set; }

        [DataMember]
        public int SprintNumber { get; set; }

        [DataMember]
        public string SprintName { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        //Relationships

        [DataMember]
        public Account ScrumMaster { get; set; }

        [DataMember]
        public ICollection<Account> SprintMembers { get; set; }


        public ICollection<Project> SprintFor { get; set; } 

        [DataMember]
        public ICollection<UserStory> UserStories { get; set; }

        [DataMember]
        public Project Project { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return SprintId; }
            set { SprintId = value; }
        }

        #endregion
    }
}