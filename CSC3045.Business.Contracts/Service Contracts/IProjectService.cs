﻿using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Project AddProject(Project project);

        [OperationContract]
        [FaultContract(typeof (NotFoundException))]
        Project GetProjectInfo(int projectId);

        [OperationContract]
        IEnumerable<Project> GetProjectsByProjectManager(int projectManagerId);

        [OperationContract]
        IEnumerable<Project> GetProjectsByProductOwner(int productOwnerId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateProjectInfo(Project project);

        [OperationContract]
        [FaultContract(typeof (NotFoundException))]
        IEnumerable<Project> GetProjectsByAccount(int accountId);
    }
}