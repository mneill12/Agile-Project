using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using Core.Common.ServiceModel;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof(IAccountService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountClient : ClientBase<IAccountService>, IAccountService
    {
        public Account GetAccountInfo(string loginEmail)
        {
            return Channel.GetAccountInfo(loginEmail);
        }

        public Account GetAccountInfoWithPassword(string loginEmail, string password)
        {
            return Channel.GetAccountInfoWithPassword(loginEmail, password);
        }

        public Account RegisterAccount(Account account)
        {
            return Channel.RegisterAccount(account);
        }

        public void UpdateAccountInfo(Account account)
        {
            Channel.UpdateAccountInfo(account);
        }
    }
}
