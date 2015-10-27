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
    // UserRole LINQ Entity Queries
    public class UserRoleRepository : DataRepositoryBase<UserRole>
    {
        protected override UserRole AddEntity(CSC3045AgileContext entityContext, UserRole entity)
        {
            return entityContext.UserRoleSet.Add(entity);
        }

        protected override UserRole UpdateEntity(CSC3045AgileContext entityContext, UserRole entity)
        {
            return (from e in entityContext.UserRoleSet
                    where e.UserRoleId == entity.UserRoleId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserRole> GetEntities(CSC3045AgileContext entityContext)
        {
            return from e in entityContext.UserRoleSet
                   select e;
        }

        protected override UserRole GetEntity(CSC3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.UserRoleSet
                         where e.UserRoleId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
