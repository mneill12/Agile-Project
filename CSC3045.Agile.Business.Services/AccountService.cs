using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System.Security.Permissions;
using System.ServiceProcess;
using Core.Common;
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

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        #region IAccountService operations

        /**
         * Get all accounts, along with associated user roles
         * @return accounts - A collection of Account objects, with user roles
         */
        public ICollection<Account> GetAllAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                ICollection<Account> accounts = accountRepository.GetAccounts();
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

        #endregion
    }
}
