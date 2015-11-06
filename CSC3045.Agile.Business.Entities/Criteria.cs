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
    public class Criteria : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int CriteriaId { get; set; }

        [DataMember]
        public string CriteriaType { get; set; }

        [DataMember]
        public string CriteriaOutline { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return CriteriaId; }
            set { CriteriaId = value; }
        }

        #endregion
    }


}
