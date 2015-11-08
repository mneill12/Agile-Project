using System.ComponentModel.Composition;
using System.ServiceModel;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof (IAuthenticationService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AuthenticationClient : ClientBase<IAuthenticationService>, IAuthenticationService
    {
        public Account AuthenticateUser(string email, string hashedPassword)
        {
            return Channel.AuthenticateUser(email, hashedPassword);
        }
    }
}