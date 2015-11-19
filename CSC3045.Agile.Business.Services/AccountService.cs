using System.Collections.Generic;
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

        /// <summary>
        /// Used for testing, this service has the data repository initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        public AccountService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        /// <summary>
        /// Used for testing, this service has the business engine initialized through dependency injection
        /// </summary>
        /// <param name="businessEngineFactory"></param>
        public AccountService(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        /// <summary>
        /// Used for testing, this service has the data repository & business engine initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        /// <param name="businessEngineFactory"></param>
        public AccountService(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        /// <summary>
        /// Get all accounts with for a specific userrole id
        /// </summary>
        /// <param name="role">The id of the userrole</param>
        /// <returns>A list of account entities</returns>
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

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns>A collection of account entities</returns>
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

        /// <summary>
        /// Get all accounts, along with associated user roles
        /// </summary>
        /// <returns>A collection of Account entities with user roles</returns>
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

        /// <summary>
        /// Get a single account by login email address
        /// </summary>
        /// <param name="loginEmail">A unique login email address for the account</param>
        /// <returns>An account entity</returns>
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

        /// <summary>
        /// Get a single account with login email address and password
        /// </summary>
        /// <param name="loginEmail">The login email to check</param>
        /// <param name="password">The password that should match the stored one</param>
        /// <returns>An account entity</returns>
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

        /// <summary>
        /// Register a new account
        /// </summary>
        /// <param name="account">The account entity to add to the database</param>
        /// <returns>The account entity from the database with the generated account Id</returns>
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

        /// <summary>
        /// Updates an account with a matching account id
        /// </summary>
        /// <param name="account">The account entity to update</param>
        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateAccountInfo(Account account)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                
                var updatedAccount = accountRepository.Update(account);

                if (updatedAccount == null)
                {
                    var ex = new NotFoundException(string.Format("Account {0} could not be updated or found", account.LoginEmail));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
            });
        }

        /// <summary>
        /// Checks if an account already exists
        /// </summary>
        /// <param name="loginEmail">The email address to check</param>
        /// <returns>Returns true if the account already exists to prevent accounts being overwritten</returns>
        public bool IsAccountAlreadyCreated(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountEngine = _BusinessEngineFactory.GetBusinessEngine<IAccountEngine>();

                return accountEngine.IsAccountAlreadyCreated(loginEmail);
            });
        }

        /// <summary>
        /// Gets a list of available user roles
        /// </summary>
        /// <returns>A list of user roles</returns>
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

        //TODO: Move to tasks service
        /// <summary>
        /// Gets a list of owned tasks for an account id
        /// </summary>
        /// <param name="accountId">The account id to get a list of tasks for</param>
        /// <returns>A list of story tasks</returns>
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

        /// <summary>
        /// Gets a list of accounts based on a role and email address
        /// </summary>
        /// <param name="role">The role address to check</param>
        /// <param name="email">The email address to check</param>
        /// <returns>A list of accounts</returns>
        public ICollection<Account> GetByRoleAndEmail(string role, string email)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                ICollection<Account> foundAccounts = accountRepository.GetUsersByRoleAndName(role, email);
                if (foundAccounts == null)
                {
                    NotFoundException ex = new NotFoundException("Error - There are no accounts to get");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return foundAccounts.ToList();
            });
        }
    }
}