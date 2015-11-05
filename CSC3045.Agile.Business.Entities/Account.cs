using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ISet<UserRole> UserRoles { get; set; }
        public ISet<Project> AssocicatedProjects { get; set; }
        public ISet<Sprint> AssociatedSprints { get; set; }
        public ISet<PlanningPokerSession> AssociatedPlanningPokerSessions { get; set; } 


        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return AccountId; }
            set { AccountId = value; }
        }

        #endregion
    }
}
