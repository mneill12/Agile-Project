using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Data;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Account LINQ Entity Queries, using MEF for DI with the client using the IAccountRepository
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
    {
        protected override Account AddEntity(CSC3045AgileContext entityContext, Account entity)
        {
            return entityContext.AccountSet.Add(entity);
        }

        protected override Account UpdateEntity(CSC3045AgileContext entityContext, Account entity)
        {
            return (from e in entityContext.AccountSet
                where e.AccountId == entity.AccountId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<Account> GetEntities(CSC3045AgileContext entityContext)
        {
            return from e in entityContext.AccountSet
                select e;
        }

        protected override Account GetEntity(CSC3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.AccountSet
                where e.AccountId == id
                select e);

            var results = query.FirstOrDefault();

            return results;
        }

        // Gets account based on e-mail address instead of Account Id
        public Account GetByLogin(string login)
        {
            using (CSC3045AgileContext entityContext = new CSC3045AgileContext())
            {
                return (from a in entityContext.AccountSet
                    where a.LoginEmail == login
                    select a).FirstOrDefault();
            }
        }
    }
}
