using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using Core.Common.Utils;
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
                var query = (from p in entityContext.ProjectSet
                             .Include(a => a.AllUsers)
                             .Include(a => a.Developers)
                             .Include(a => a.ScrumMasters)
                             where p.AllUsers.Select(a => a.AccountId).Contains(accountId)
                             select p);
                return query.ToList<Project>();
            }

        }

        public Project AddProjectWithUsers(Project project)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                // The worst, the ABSOLUTE WORST!!!!!
                project.ProjectName = project.ProjectName;
                project.ProjectStartDate = project.ProjectStartDate;
                project.ProductOwner = entityContext.AccountSet.Single(a => a.AccountId == project.ProductOwner.AccountId);
                project.ProjectManager = entityContext.AccountSet.Single(a => a.AccountId == project.ProjectManager.AccountId);
                project.Backlog = project.Backlog;
                project.Burndowns = project.Burndowns;
                project.Sprints = project.Sprints;
                project.ScrumMasters = project.ScrumMasters.Select(scrumMaster => entityContext.AccountSet.Single(a => a.AccountId == scrumMaster.AccountId)).ToList();
                project.Developers = project.Developers.Select(developer => entityContext.AccountSet.Single(a => a.AccountId == developer.AccountId)).ToList();
                project.AllUsers = project.AllUsers.Select(user => entityContext.AccountSet.Single(a => a.AccountId == user.AccountId)).ToList();

                Project addedProject = AddEntity(entityContext, project);
                entityContext.SaveChanges();

                return addedProject;
            }

        }

        public Project UpdateProjectWithUsers(Project project)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                // The worst, the ABSOLUTE WORST!!!!!
                var updatedProject = entityContext.ProjectSet.Single(p => p.ProjectId == project.ProjectId);

                updatedProject.ProjectName = project.ProjectName;
                updatedProject.ProjectStartDate = project.ProjectStartDate;
                updatedProject.ProductOwner = entityContext.AccountSet.Single(a => a.AccountId == project.ProductOwner.AccountId);
                updatedProject.ProjectManager = entityContext.AccountSet.Single(a => a.AccountId == project.ProjectManager.AccountId);
                updatedProject.Backlog = project.Backlog;
                updatedProject.Burndowns = project.Burndowns;
                updatedProject.Sprints = project.Sprints;
                updatedProject.ScrumMasters = project.ScrumMasters.Select(scrumMaster => entityContext.AccountSet.Single(a => a.AccountId == scrumMaster.AccountId)).ToList();
                updatedProject.Developers = project.Developers.Select(developer => entityContext.AccountSet.Single(a => a.AccountId == developer.AccountId)).ToList();
                updatedProject.AllUsers = project.AllUsers.Select(user => entityContext.AccountSet.Single(a => a.AccountId == user.AccountId)).ToList();

                entityContext.SaveChanges();

                return updatedProject;
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
                    .Include(a => a.AllUsers)
                    .Include(a => a.Developers)
                    .Include(a => a.ScrumMasters)
                    .Include(a => a.Sprints)
                    .Include(a => a.Burndowns)
                    .ToList();
        }

        protected override Project GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.ProjectSet
                    .Include(a => a.AllUsers)
                    .Include(a => a.Developers)
                    .Include(a => a.ScrumMasters)
                    .Include(a => a.Sprints)
                    .Include(a => a.Burndowns)
                    .FirstOrDefault(p => p.ProjectId == id);
        }

        #endregion
    }
}