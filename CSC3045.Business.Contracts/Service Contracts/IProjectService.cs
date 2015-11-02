using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts.Service_Contracts
{
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool AddProject(Project project);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Project GetProjectInfo(int projectId);

        [OperationContract]
        IEnumerable<Project> GetProjectsByProjectManager(int projectManagerId);

        [OperationContract]
        IEnumerable<Project> GetProjectsByProductOwner(int productOwnerId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateProjectInfo(Project project);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<Project> GetProjectsByAccount(int accountId)
    }
}
