using System;
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
        [Import] private IBusinessEngineFactory _BusinessEngineFactory;

        [Import] private IDataRepositoryFactory _DataRepositoryFactory;

        public ProjectService()
        {
        }

        public ProjectService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public ProjectService(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        public ProjectService(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        #region IProjectService operations

        public Project GetProjectInfo(int projectId)
        {
            var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            var projectEntity = projectRepository.GetByProjectId(projectId);
            if (projectEntity == null)
            {
                var ex = new NotFoundException(string.Format("Project with projectId {0} could not be found", projectId));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return projectEntity;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateProjectInfo(Project project)
        {
            throw new NotImplementedException();
        }

        public Project AddProject(Project project)
        {
            var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            var p = projectRepository.Add(project);

            return p;
        }

        public IEnumerable<Project> GetProjectsByProjectManager(int projectManagerId)
        {
            var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            return projectRepository.GetManagedProjectsByAccount(projectManagerId);
        }

        public IEnumerable<Project> GetProjectsByProductOwner(int productOwnerId)
        {
            var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            return projectRepository.GetOwnedProjectsByAccount(productOwnerId);
        }

        public IEnumerable<Project> GetProjectsByAccount(int accountId)
        {
            var projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            var projects = projectRepository.GetProjectsByAccount(accountId);

            return projects;
        }

        #endregion
    }
}