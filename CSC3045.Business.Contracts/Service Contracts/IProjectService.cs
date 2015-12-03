using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        ICollection<Project> GetProjectsForProjectManager(int projectManagerId);

        [OperationContract]
        ICollection<Project> GetProjectsForProductOwner(int productOwnerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<Project> GetProjectsForAccount(int accountId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Project CreateProject(Project project);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void SaveProject(Project project);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateProjectInfo(Project project);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Project GetProjectInfo(int projectId);
    }
}