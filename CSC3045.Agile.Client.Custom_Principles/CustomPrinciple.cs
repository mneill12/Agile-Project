using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using CSC3045.Agile.Client.Entities;


namespace CSC3045.Agile.Client.CustomPrinciples
{
    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity _identity;

        public CustomIdentity Identity
        {
            get { return _identity ?? new AnonymousIdentity(); }
            set { _identity = value; }
        }

        IIdentity IPrincipal.Identity
        {
            get { return this.Identity; }
        }

        public bool IsInRole(string role)
        {
            bool roleFound = false;
            foreach(UserRole userRole in _identity.Roles){

                if (userRole.UserRoleName == role)
                    roleFound = true;
            }

            return roleFound;
        }
  
    }
}
