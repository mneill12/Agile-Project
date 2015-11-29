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

        public IList<Skill> GetAllSkills()
        {
            return Channel.GetAllSkills();
        }

        public ICollection<StoryTask> GetOwnedTasks(int accountId)
        {
            return Channel.GetOwnedTasks(accountId);
        } 


        public IEnumerable<Account> GetByUserRole(string role)
        {
            return Channel.GetByUserRole(role);
        }

        public ICollection<Account> GetByRoleAndEmail(string role, string email)
        {
            return Channel.GetByRoleAndEmail(role, email);
        }

        public ICollection<Account> GetDevelopersBySkill(string skillName)
        {
            return Channel.GetDevelopersBySkill(skillName);
        }

        public List<Account> GetDevelopersBySkills(List<string> skillNames)
        {
            return Channel.GetDevelopersBySkills(skillNames);
        }
    }
}