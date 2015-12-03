﻿using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IStoryTaskService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<StoryTask> GetAllTasks();

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
        IList<UserRole> GetAllUserRoles();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IList<Skill> GetAllSkills();

        [OperationContract]

        [FaultContract(typeof(NotFoundException))]
        ICollection<StoryTask> GetOwnedTasks(int accountId);

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