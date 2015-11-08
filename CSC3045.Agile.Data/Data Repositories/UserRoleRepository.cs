using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // UserRole LINQ Entity Queries
    [Export(typeof (IUserRoleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserRoleRepository : DataRepositoryBase<UserRole>, IUserRoleRepository
    {
        public IList<UserRole> GetAllUserRoles()
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.UserRoleSet.ToList();
            }
        }

        protected override UserRole AddEntity(Csc3045AgileContext entityContext, UserRole entity)
        {
            return entityContext.UserRoleSet.Add(entity);
        }

        protected override UserRole UpdateEntity(Csc3045AgileContext entityContext, UserRole entity)
        {
            return (from e in entityContext.UserRoleSet
                where e.UserRoleId == entity.UserRoleId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<UserRole> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.UserRoleSet
                select e;
        }

        protected override UserRole GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.UserRoleSet
                where e.UserRoleId == id
                select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}