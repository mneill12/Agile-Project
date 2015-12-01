using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Entities
{
    [DataContract]
    public class ChatMessage : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        public int MessageId { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public Account Sender { get; set; }

        [DataMember]
        public string Message { get; set; }

        #region IIdentifiableEntity members

        public int EntityId
        {
            get { return MessageId; }
            set { MessageId = value; }
        }

        #endregion
    }
}