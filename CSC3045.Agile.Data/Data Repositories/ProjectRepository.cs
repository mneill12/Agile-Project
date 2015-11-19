using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Project LINQ Entity Queries
    [Export(typeof (IProjectRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectRepository : DataRepositoryBase<Project>, IProjectRepository
    {

        #region Specific Methods

        public ICollection<Project> GetProjectsForProjectManager(int projectManagerId)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return (from p in entityContext.ProjectSet
                        where p.ProjectManager.AccountId == projectManagerId
                        select p).ToList();
            }
        }

        public ICollection<Project> GetProjectsForProductOwner(int productOwnerId)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return (from p in entityContext.ProjectSet
                        where p.ProductOwner.AccountId == productOwnerId
                        select p).ToList();
            }
        }

        public ICollection<Project> GetProjectsForAccount(int accountId)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return (from p in entityContext.ProjectSet
                    where p.AllUsers.Select(a => a.AccountId).Contains(accountId)
                    select p).ToList();
            }
        }

        #endregion

        #region CRUD Methods

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
            return entityContext.ProjectSet
                    .ToList();
        }

        protected override Project GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.ProjectSet
                    .FirstOrDefault(p => p.ProjectId == id);
        }

        #endregion
    }
}