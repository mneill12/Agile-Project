using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities.XMLEntities
{
    [DataContract]
    public class XMLProject : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public XMLAccount ProjectManager { get; set; }

        [DataMember]
        public XMLAccount ProductOwner { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public DateTime ProjectStartDate { get; set; }

        [DataMember]
        public XMLBacklog Backlog { get; set; }

        [DataMember]
        public List<XMLSprint> Sprints { get; set; }

        [DataMember]
        public List<XMLBurndown> Burndowns { get; set; }

        [DataMember]
        [InverseProperty("UserFor")]
        public List<XMLAccount> AllUsers { get; set; }

        [DataMember]
        [InverseProperty("ScrumMasterFor")]
        public List<XMLAccount> ScrumMasters { get; set; }

        [DataMember]
        [InverseProperty("DeveloperFor")]
        public List<XMLAccount> Developers { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return ProjectId; }
            set { ProjectId = value; }
        }

        #endregion
    }
}