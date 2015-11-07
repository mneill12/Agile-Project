using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Authentication;
using System.ServiceModel;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System.Security.Permissions;
using System.ServiceProcess;
using Core.Common;
using CSC3045.Agile.Business.Common;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class AccountService : ServiceBase, IAccountService
    {
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

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

        #region IAccountService operations

        /**
         * Get all accounts
         * @return accounts - A collection of Account objects
         */
        public ICollection<Account> GetAllAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                ICollection<Account> accounts = accountRepository.GetAllAccounts();
                if (accounts == null)
                {
                    NotFoundException ex = new NotFoundException("Error retrieving Accounts from database");
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
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                ICollection<Account> accounts = accountRepository.GetAllAccountsWithUserRoles();
                if (accounts == null)
                {
                    NotFoundException ex = new NotFoundException("Error retrieving Accounts from database");
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
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                Account accountEntity = accountRepository.GetByLogin(loginEmail);
                if (accountEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Account with login {0} is not in database", loginEmail));
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
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                Account accountEntity = accountRepository.GetByLoginAndPasswordWithUserRoles(loginEmail, password);
                if (accountEntity == null)
                {
                    AuthenticationException ex = new AuthenticationException(string.Format("An invalid combination was entered or the account cannot be found"));
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
                    IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

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
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                Account updatedAccount = accountRepository.Update(account);
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
                IAccountEngine accountEngine = _BusinessEngineFactory.GetBusinessEngine<IAccountEngine>();

                return accountEngine.IsAccountAlreadyCreated(loginEmail);
            });
        }

        public ICollection<UserRole> GetAllUserRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserRoleRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IUserRoleRepository>();

                ICollection<UserRole> userRoles = userRoleRepository.GetAllUserRoles();
                if (userRoles == null)
                {
                    NotFoundException ex = new NotFoundException("Error - There are no user roles to get");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userRoles;
            });
        }

        #endregion


        public IEnumerable<Account> GetByUserRole(int permissionLevel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                IEnumerable<Account> accounts = accountRepository.GetByUserRole(permissionLevel);
                if(accounts == null)
                {
                    NotFoundException ex = new NotFoundException("Error retrieving Accounts from database");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accounts;
            });
        }

        public ICollection<StoryTask> GetOwnedTasks(Account account)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IStoryTaskRepository userRoleRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                IEnumerable<StoryTask> userRoles = userRoleRepository.GetTasksByOwner(account);
                if (userRoles == null)
                {
                    NotFoundException ex = new NotFoundException("Error - There are no tasks to get");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userRoles.ToList();
            });
        }
    }
}
