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
        ICollection<Account> GetAllAccounts();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<Account> GetAllAccountsWithUserRoles();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Account GetAccountInfo(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Account GetAccountInfoWithPasswordAndUserRoles(string loginEmail, string password);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account RegisterAccount(Account account);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateAccountInfo(Account account);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<UserRole> GetAllUserRoles();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<Account> GetByUserRole(int permissionLevel);

    }
}
