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
        //CRUD

        //Create
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Sprint AddSprint(Sprint sprint);

        //Read
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Sprint GetSprintInfo(int sprintId);

        //Update
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateSprintInfo(Sprint sprint);

        //Custom

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ICollection<Sprint> GetAllSprints();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<Sprint> GetSprintsForProjectId(int projectId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<Sprint> GetSprintsForAccountId(int accountId);
    }
}