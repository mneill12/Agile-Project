using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Client.Entities;


namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface ISprintService : IServiceContract
    {

        [OperationContract]
        ICollection<Sprint> GetSprintForProject(int projectId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Sprint AddSprint(Sprint sprint);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Sprint GetSprintInfo(int sprintId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Sprint GetSprintStartDate(int sprintId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Sprint GetSprintEndDate(int sprintId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateSprintInfo(Sprint sprint);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<Sprint> GetSprintsByAccount(int accountId);
    }
}