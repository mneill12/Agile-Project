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
        public ICollection<Account> GetAllAccounts()
        {
            return Channel.GetAllAccounts();
        }

        public ICollection<Account> GetAllAccountsWithUserRoles()
        {
            return Channel.GetAllAccountsWithUserRoles();
        }

        public Account GetAccountInfo(string loginEmail)
        {
            return Channel.GetAccountInfo(loginEmail);
        }

        public Account GetAccountInfoWithPasswordAndUserRoles(string loginEmail, string password)
        {
            return Channel.GetAccountInfoWithPasswordAndUserRoles(loginEmail, password);
        }

        public Account RegisterAccount(Account account)
        {
            return Channel.RegisterAccount(account);
        }

        public void UpdateAccountInfo(Account account)
        {
            Channel.UpdateAccountInfo(account);
        }

        public ICollection<UserRole> GetAllUserRoles()
        {
            return Channel.GetAllUserRoles();
        }

        public ICollection<StoryTask> GetOwnedTasks(Account account)
        {
            return Channel.GetOwnedTasks(account);
        } 


        public IEnumerable<Account> GetByUserRole(int permissionLevel)
        {
            return Channel.GetByUserRole(permissionLevel);
        }
    }
}
