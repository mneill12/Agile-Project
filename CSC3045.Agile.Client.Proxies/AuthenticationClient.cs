using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using Core.Common.ServiceModel;
using Core.Common.Utils;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof(IAuthenticationService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]

    public class AuthenticationClient : ClientBase<IAuthenticationService>, IAuthenticationService
    {
    
        public Account AuthenticateUser(string email, string password )
        {
            try
            {
                return Channel.AuthenticateUser(email, password);
            }
            catch (Exception ex)
            {
                String Status = string.Format("ERROR: {0}", ex.Message);
                return new Account();
            }
        }
    }
}
