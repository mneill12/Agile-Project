using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IAccountService
    {
        //CRUD

        //Create
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account RegisterAccount(Account account);

        //Read
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Account GetAccountInfo(string loginEmail);

        //Update
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateAccountInfo(Account account);

        //Relationships

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IList<UserRole> GetAllUserRoles();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IList<Skill> GetAllSkills();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<StoryTask> GetOwnedTasks(int accountId);

        //Custom

        [OperationContract]
        [FaultContract(typeof (NotFoundException))]
        ICollection<Account> GetAllAccounts();

        [OperationContract]
        [FaultContract(typeof (NotFoundException))]
        ICollection<Account> GetAllAccountsWithUserRoles();

        [OperationContract]
        [FaultContract(typeof (NotFoundException))]
        Account GetAccountInfoWithPasswordAndUserRoles(string loginEmail, string password);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<Account> GetByUserRole(string role);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<Account> GetByRoleAndEmail(string role, string email);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<Account> GetDevelopersBySkill(string skillName);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        List<Account> GetDevelopersBySkills(List<string> skillNames);
    }
}