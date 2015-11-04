using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;
using CSC3045.Agile.Client.Entities;


namespace CSC3045.Agile.Client.Contracts
{
    [DataContract]
    public class User
    {
        public User()
        {

        }

        public User(string email, ISet<UserRole> roles)
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
        public ISet<UserRole> Roles
        {
            get;
            set;
        }
    }
}
