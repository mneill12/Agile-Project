using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;


namespace Core.Common.Utils
{
    [DataContract]
    public class User
    {
        public User()
        {

        }

        public User(string email, string[] roles)
        {
            Email = email;
            Roles = roles;
        }

        [DataMember]
        public string Email
        {
            get;
            set;
        }
        [DataMember]
        public string[] Roles
        {
            get;
            set;
        }
    }
}
