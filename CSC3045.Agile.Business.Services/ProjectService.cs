using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ProjectService : ServiceBase, IProjectService
    {
        [Import] private IDataRepositoryFactory _DataRepositoryFactory;

        public ProjectService()
        {
        }

        /// <summary>
        /// Used for testing, this service has the data repository initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        public ProjectService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public ICollection<Project> GetProjectsForProjectManager(int projectManagerId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                return projectRepository.GetProjectsForProjectManager(projectManagerId);
            });
        }

        public ICollection<Project> GetProjectsForProductOwner(int productOwnerId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                return projectRepository.GetProjectsForProductOwner(productOwnerId);
            });
        }

        public ICollection<Project> GetProjectsForAccount(int accountId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                return projectRepository.GetProjectsForAccount(accountId);
            });
        }

        public Project CreateProject(Project project)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                return projectRepository.AddProjectWithUsers(project);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void SaveProject(Project project)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                var updatedProject = projectRepository.Update(project);

                if (updatedProject == null)
                {
                    var ex = new NotFoundException(string.Format("Project with id {0} could not be updated or found", project.ProjectId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
            });
           
        }

        /// <summary>
        /// Updates the project by getting the latest project from the database and updating it in the same context
        /// </summary>
        /// <param name="project">The updated project details from the client</param>
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateProjectInfo(Project project)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                var updatedProject = projectRepository.UpdateProjectWithUsers(project);

                if (updatedProject == null)
                {
                    var ex = new NotFoundException(string.Format("Project with id {0} could not be updated or found", project.ProjectId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
            });
        }

        public Project GetProjectInfo(int projectId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                var projectEntity = projectRepository.Get(projectId);

                if (projectEntity == null)
                {
                    var ex = new NotFoundException(string.Format("Project with id {0} is not in database", projectId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return projectEntity;
            });
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                return projectRepository.Get();
            });
        }


        public void AddUserStoryToProject(int projectId)
        {
            var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            projectRepository.AddBacklogStoryToProject(projectId);
        }

        public void AddUserStoryToProject(int projectId, UserStory userStory)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

                projectRepository.AddStoryToProject(projectId, userStory.UserStoryId);
            });
        }
    }
}