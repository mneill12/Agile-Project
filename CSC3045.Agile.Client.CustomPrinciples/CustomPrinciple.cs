using System.Security.Principal;

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
            get { return Identity; }
        }

        public bool IsInRole(string role)
        {
            var roleFound = false;
            foreach (var userRole in _identity.Roles)
            {
                if (userRole.UserRoleName == role)
                    roleFound = true;
            }

            return roleFound;
        }
    }
}