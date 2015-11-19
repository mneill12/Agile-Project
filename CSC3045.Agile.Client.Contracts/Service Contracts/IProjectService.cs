﻿using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IProjectService : IServiceContract
    {
        [OperationContract]
        IEnumerable<Project> GetProjectsForProjectManager(int projectManagerId);

        [OperationContract]
        IEnumerable<Project> GetProjectsForProductOwner(int productOwnerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<Project> GetProjectsForAccount(int accountId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Project CreateProject(Project project);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateProjectInfo(Project project);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Project GetProjectInfo(int projectId);
    }
}