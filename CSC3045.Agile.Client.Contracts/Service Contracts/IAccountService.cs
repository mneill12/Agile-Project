using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using CSC3045.Agile.Client.Entities;
using Core.Common.Contracts;
using Core.Common.Exceptions;

namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IAccountService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Account GetAccountInfo(string loginEmail, string password);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account RegisterAccount(Account account);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateAccountInfo(Account account);

    }
}
