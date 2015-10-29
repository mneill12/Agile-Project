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

        public ICollection<Account> GetAllAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                ICollection<Account> accountsWithChildren = accountRepository.GetAllAccounts();
                if (accountsWithChildren == null)
                {
                    NotFoundException ex = new NotFoundException("Error retrieving Accounts from database");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountsWithChildren;
            });
        }

        public ICollection<Account> GetAllAccountsWithChildren()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                ICollection<Account> accountsWithChildren = accountRepository.GetAccountsWithChildren();
                if (accountsWithChildren == null)
                {
                    NotFoundException ex = new NotFoundException("Error retrieving Accounts from database");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountsWithChildren;
            });
        }

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

        public Account GetAccountInfo(string loginEmail, string password)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                Account accountEntity = accountRepository.GetByLogin(loginEmail, password);
                if (accountEntity == null)
                {
                    AuthenticationException ex = new AuthenticationException(string.Format("An invalid combination was entered or the account cannot be found"));
                    throw new FaultException<AuthenticationException>(ex, ex.Message);
                }

                return accountEntity;
            });
        }

        public Account RegisterAccount(Account account)
        {
            if (!IsAccountAlreadyCreated(account.LoginEmail))
            {
                ExecuteFaultHandledOperation(() =>
                {
                    IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                    return accountRepository.Add(account);
                });
            }

            return null;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateAccountInfo(Account account)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                Account updatedAccount = accountRepository.Update(account);
            });
        }

        public bool IsAccountAlreadyCreated(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountEngine accountEngine = _BusinessEngineFactory.GetBusinessEngine<IAccountEngine>();

                return accountEngine.IsAccountAlreadyCreated(loginEmail);
            });
        }

        #endregion
    }
}
