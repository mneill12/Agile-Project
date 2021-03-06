﻿using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of AccountRepository
    public interface IAccountRepository : IDataRepository<Account>
    {
        Account GetByLogin(string login);

        Account GetByLoginAndPassword(string login, string password);

        Account GetByLoginAndPasswordWithUserRoles(string login, string password);

        IEnumerable<Account> GetByUserRole(string role);

        ICollection<Account> GetAllAccounts();

        ICollection<Account> GetAllAccountsWithUserRoles();

        ICollection<Account> GetUsersByRoleAndName(string role, string email);

        ICollection<Account> GetDevelopersBySkill(string skill);

        List<Account> GetDevelopersBySkills(List<string> skills);


    }
}