using System.Collections.Generic;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using System.Data.Entity;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Project LINQ Entity Queries
    public class ProjectRepository : DataRepositoryBase<Project>, IProjectRepository
    {
        protected override Project AddEntity(Csc3045AgileContext entityContext, Project entity)
        {
            return entityContext.ProjectSet.Add(entity);
        }

        protected override Project UpdateEntity(Csc3045AgileContext entityContext, Project entity)
        {
            return (from e in entityContext.ProjectSet
                    where e.ProjectId == entity.ProjectId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Project> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.ProjectSet
                   select e;
        }

        protected override Project GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.ProjectSet
                         where e.ProjectId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //TODO @Marty - Is there a reason for this? GetEntity above does the same thing, as we override EntityId with ProjectId?
        //Gets project based on Project Id
        public Project GetByProjectId(int projectId)
        {
            using (Csc3045AgileContext entityContext = new Csc3045AgileContext())
            {
                return GetEntity(entityContext, projectId);
            }
        }


        public IEnumerable<Project> GetManagedProjectsByAccount(int projectManagerId)
        {
            using(var entityContext = new Csc3045AgileContext())
            {
                return entityContext.ProjectSet
                    .Include(a => a.ProjectManager)
                    .Include(a => a.ProductOwner)
                    .Where(a => a.ProjectManager.AccountId == projectManagerId)
                    .ToList();
            }
        }

        public IEnumerable<Project> GetOwnedProjectsByAccount(int productOwnerId)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.ProjectSet
                    .Include(a => a.ProjectManager)
                    .Include(a => a.ProductOwner)
                    .Where(a => a.ProductOwner.AccountId == productOwnerId)
                    .ToList();
            }
        }
    }
}
