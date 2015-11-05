using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IProjectService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Project AddProject(Project project);

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
        IEnumerable<Project> GetProjectsByAccount(int accountId);
    }
}
