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
        [FaultContract(typeof (NotFoundException))]
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
        ICollection<StoryTask> GetOwnedTasks(Account account);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<Account> GetByUserRole(int permissionLevel);

    }
}
