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
        //CRUD

        //Create
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Project CreateProject(Project project);

        //Read
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Project GetProjectInfo(int projectId);

        //Update
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateProjectInfo(Project project);

        //Custom
        [OperationContract]
        ICollection<Project> GetProjectsForProjectManager(int projectManagerId);

        [OperationContract]
        ICollection<Project> GetProjectsForProductOwner(int productOwnerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<Project> GetProjectsForAccount(int accountId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AddUserStoryToProject(int projectId, UserStory userStory);

        [OperationContract]
        void SaveToXML(Project project);
    }
}