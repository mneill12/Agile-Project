﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Business.Contracts;
using Core.Common.Contracts;
using System.ComponentModel.Composition;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using CSC3045.Agile.Business.Entities;
using Core.Common.Exceptions;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class ProjectService : ServiceBase, IProjectService
    {

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

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

        #region IProjectService operations

        public Entities.Project GetProjectInfo(int projectId)
        {
            IProjectRepository projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            Project projectEntity = projectRepository.GetByProjectId(projectId);
            if(projectEntity == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("Project with projectId {0} could not be found", projectId));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return projectEntity;
        }

        public void UpdateProjectInfo(Entities.Project project)
        {
            throw new NotImplementedException();
        }

        public Project AddProject(Project project)
        {
            IProjectRepository projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            Project p = projectRepository.Add(project);

            return p;
        }

        public IEnumerable<Project> GetProjectsByProjectManager(int projectManagerId)
        {
            IProjectRepository projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            return projectRepository.GetManagedProjectsByAccount(projectManagerId);
        }

        public IEnumerable<Project> GetProjectsByProductOwner(int productOwnerId)
        {
            IProjectRepository projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            return projectRepository.GetOwnedProjectsByAccount(productOwnerId);
        }

        public IEnumerable<Project> GetProjectsByAccount(int accountId)
        {
            IProjectRepository projectRepository = _DataRepositoryFactory.GetDataRepository<IProjectRepository>();

            IEnumerable<Project> projects = projectRepository.GetProjectsByAccount(accountId);

            return projects;
        }

        #endregion
    }
}
