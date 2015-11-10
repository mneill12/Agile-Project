﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Authentication;
using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Common;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class AccountService : ServiceBase, IAccountService
    {
        [Import] private IBusinessEngineFactory _BusinessEngineFactory;

        [Import] private IDataRepositoryFactory _DataRepositoryFactory;

        public AccountService()
        {
        }

        public AccountService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public AccountService(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        public AccountService(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }


        public IEnumerable<Account> GetByUserRole(string role)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                var accounts = accountRepository.GetByUserRole(role);
                if (accounts == null)
                {
                    var ex = new NotFoundException("Error retrieving Accounts from database");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accounts;
            });
        }

        #region IAccountService operations

        /**
         * Get all accounts
         * @return accounts - A collection of Account objects
         */

        public ICollection<Account> GetAllAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                var accounts = accountRepository.GetAllAccounts();
                if (accounts == null)
                {
                    var ex = new NotFoundException("Error retrieving Accounts from database");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accounts;
            });
        }

        /**
         * Get all accounts, along with associated user roles
         * @return accounts - A collection of Account objects, with user roles
         */

        public ICollection<Account> GetAllAccountsWithUserRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                var accounts = accountRepository.GetAllAccountsWithUserRoles();
                if (accounts == null)
                {
                    var ex = new NotFoundException("Error retrieving Accounts from database");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accounts;
            });
        }

        /**
         * Get a single account by login (email address)
         * @parameter loginEmail - the email to retrieve account for
         */

        public Account GetAccountInfo(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                var accountEntity = accountRepository.GetByLogin(loginEmail);
                if (accountEntity == null)
                {
                    var ex =
                        new NotFoundException(string.Format("Account with login {0} is not in database", loginEmail));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountEntity;
            });
        }

        /**
         * Get a single account be login (email) and password combined
         * @parameter loginEmail - the login email to check
         * @parameter password - the password that should match a stored one
         */

        public Account GetAccountInfoWithPasswordAndUserRoles(string loginEmail, string password)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                var accountEntity = accountRepository.GetByLoginAndPasswordWithUserRoles(loginEmail, password);
                if (accountEntity == null)
                {
                    var ex =
                        new AuthenticationException("An invalid combination was entered or the account cannot be found");
                    throw new FaultException<AuthenticationException>(ex, ex.Message);
                }

                return accountEntity;
            });
        }

        /**
         * Register a new account
         * @parameter account - the new account object to store
         */

        public Account RegisterAccount(Account account)
        {
            if (!IsAccountAlreadyCreated(account.LoginEmail))
            {
                return ExecuteFaultHandledOperation(() =>
                {
                    var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                    return accountRepository.Add(account);
                });
            }

            return null;
        }

        /**
         * Update an account
         * @parameter account - the updated account object to store
         */

        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateAccountInfo(Account account)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                var updatedAccount = accountRepository.Update(account);
            });
        }

        /**
         * Check if an account already exists
         * @parameter loginEmail - The email address to check for existence
         */

        public bool IsAccountAlreadyCreated(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountEngine = _BusinessEngineFactory.GetBusinessEngine<IAccountEngine>();

                return accountEngine.IsAccountAlreadyCreated(loginEmail);
            });
        }

        public IList<UserRole> GetAllUserRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                var userRoles = userRoleRepository.GetAllUserRoles();
                if (userRoles == null)
                {
                    var ex = new NotFoundException("Error - There are no user roles to get");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userRoles;
            });
        }

        #endregion

        public ICollection<StoryTask> GetOwnedTasks(int accountId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IStoryTaskRepository taskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                IEnumerable<StoryTask> ownedTasks = taskRepository.GetTasksByOwner(accountId);
                if (ownedTasks == null)
                {
                    NotFoundException ex = new NotFoundException("Error - There are no tasks to get");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ownedTasks.ToList();
            });
        }

        public ICollection<Account> GetByRoleAndEmail(string role, string email)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository taskRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                ICollection<Account> foundUsers = taskRepository.GetUsersByRoleAndName(role, email);
                if (foundUsers == null)
                {
                    NotFoundException ex = new NotFoundException("Error - There are no tasks to get");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return foundUsers.ToList();
            });
        }
    }
}