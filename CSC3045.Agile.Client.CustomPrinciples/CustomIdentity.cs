using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using CSC3045.Agile.Client.Entities;


namespace CSC3045.Agile.Client.CustomPrinciples
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string email, UserRole[] roles)
        {
            Email = email;
            Roles = roles;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserRole[] Roles { get; private set; }

        #region IIdentity Members
        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Email); } }
        #endregion
    }
}