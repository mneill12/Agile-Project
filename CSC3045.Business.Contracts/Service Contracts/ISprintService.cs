using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface ISprintService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Sprint AddSprint(Sprint sprint);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Sprint GetSprintInfo(int sprintId);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateSprintInfo(Sprint sprint);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ICollection<Sprint> GetAllSprints();


    }
}