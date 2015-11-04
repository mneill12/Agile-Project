using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using Core.Common.Core;
using CSC3045.Agile.Business.Entities;


namespace CSC3045.Agile.Business.Contracts
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
