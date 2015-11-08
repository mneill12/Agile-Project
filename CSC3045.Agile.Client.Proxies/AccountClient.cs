using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof (IAccountService))]
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

        /// <summary>
        ///     List user roles set on db init
        /// </summary>
        /// <returns>[0] Developer, [1] Product Owner, [2] Scrum Master</returns>
        public IList<UserRole> GetAllUserRoles()
        {
            return Channel.GetAllUserRoles();
        }


        public IEnumerable<Account> GetByUserRole(int roleId)
        {
            return Channel.GetByUserRole(roleId);
        }
    }
}