using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Account LINQ Entity Queries, using MEF for DI with the client using the IAccountRepository
    [Export(typeof (IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
    {
        public Account GetByLoginAndPassword(string login, string password)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return (from a in entityContext.AccountSet
                    where (a.LoginEmail == login) && (a.Password == password)
                    select a).FirstOrDefault();
            }
        }


        public Account GetByLoginAndPasswordWithUserRoles(string login, string password)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return (from a in entityContext.AccountSet
                    where (a.LoginEmail == login) && (a.Password == password)
                    select a).Include(a => a.UserRoles).FirstOrDefault();
            }
        }

        // Gets account based on e-mail address instead of Account Id
        public Account GetByLogin(string login)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.AccountSet
                    .Include(a => a.UserRoles)
                    .FirstOrDefault(a => a.LoginEmail == login);
            }
        }

        // Gets accounts that have a particular user-role attached
        public IEnumerable<Account> GetByUserRole(string role)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.AccountSet
                    .Include(a => a.UserRoles)
                    .Where(a => a.UserRoles.Select(r => r.UserRoleName).Contains(role))
                    .ToList();
            }
        }

        public ICollection<Account> GetAllAccounts()
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.AccountSet.ToList();
            }
        }

        public ICollection<Account> GetAllAccountsWithUserRoles()
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.AccountSet
                    .Include(a => a.UserRoles)
                    .ToList();
            }
        }

        protected override Account AddEntity(Csc3045AgileContext entityContext, Account entity)
        {
            return entityContext.AccountSet.Add(entity);
        }

        protected override Account UpdateEntity(Csc3045AgileContext entityContext, Account entity)
        {
            return (from e in entityContext.AccountSet
                where e.AccountId == entity.AccountId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<Account> GetEntities(Csc3045AgileContext entityContext)
        {
            using (var db = new Csc3045AgileContext())
            {
                return db.AccountSet
                    .Include(a => a.UserRoles)
                    .ToList();
            }
        }

        protected override Account GetEntity(Csc3045AgileContext entityContext, int id)
        {
            using (var db = new Csc3045AgileContext())
            {
                return db.AccountSet
                    .Include(a => a.UserRoles)
                    .FirstOrDefault(a => a.AccountId == id);
            }
        }

        public ICollection<Account> GetUsersByRoleAndName(string role, string email)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.AccountSet
                    .Include(a => a.UserRoles)
                    .Where(a => a.UserRoles.Select(r => r.UserRoleName).Contains(role)
                    && a.LoginEmail.Contains(email)).ToList();
            }
        } 
    }
}