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
    // Project LINQ Entity Queries
    public class ProjectRepository : DataRepositoryBase<Project>, IProjectRepository
    {
        protected override Project AddEntity(CSC3045AgileContext entityContext, Project entity)
        {
            return entityContext.ProjectSet.Add(entity);
        }

        protected override Project UpdateEntity(CSC3045AgileContext entityContext, Project entity)
        {
            return (from e in entityContext.ProjectSet
                    where e.ProjectId == entity.ProjectId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Project> GetEntities(CSC3045AgileContext entityContext)
        {
            return from e in entityContext.ProjectSet
                   select e;
        }

        protected override Project GetEntity(CSC3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.ProjectSet
                         where e.ProjectId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //Gets project based on Project Id
        public Project GetByProjectId(int projectId)
        {
            using (CSC3045AgileContext entityContext = new CSC3045AgileContext())
            {
                return GetEntity(entityContext, projectId);
            }
        }


        public IEnumerable<Project> GetManagedProjectsByAccount(int projectManagerId)
        {
            using(CSC3045AgileContext entityContext = new CSC3045AgileContext())
            {
                var query = (from p in entityContext.ProjectSet
                             where p.ProjectManagerId == projectManagerId
                             select p);

                return query.AsEnumerable<Project>();
            }
        }

        public IEnumerable<Project> GetOwnedProjectsByAccount(int productOwnderId)
        {
            using (CSC3045AgileContext entityContext = new CSC3045AgileContext())
            {
                var query = (from p in entityContext.ProjectSet
                             where p.ProductOwnerId == productOwnderId
                             select p);

                return query.AsEnumerable<Project>();
            }
        }


        public IEnumerable<Project> GetProjectsByAccount(int accountId)
        {
            /*using(CSC3045AgileContext entityContext = new CSC3045AgileContext())
            {
                
            }*/

            throw new NotImplementedException();
        }
    }
}
