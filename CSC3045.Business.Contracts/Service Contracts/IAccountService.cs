using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Core.Common;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Account GetAccountInfo(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account RegisterAccount(Account account);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateAccountInfo(Account account);
    }
}
