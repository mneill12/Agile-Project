using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
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
using Core.Common.Utils;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;


namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private class InternalUserData
        {
            public InternalUserData( string email, string hashedPassword, string[] roles)
            {
                Email = email;
                HashedPassword = hashedPassword;
                Roles = roles;
            }
 
            public string Email
            {
                get;
                private set;
            }
 
            public string HashedPassword
            {
                get;
                private set;
            }
 
            public string[] Roles
            {
                get;
                private set;
            }
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;


        private IEnumerable<Account> getAccounts()
        {

            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IEnumerable<Account> accounts = accountRepository.Get();

            return accounts;
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public User AuthenticateUser(string email, string clearTextPassword)
        {
            String[] roles = new String[1];
            
            IEnumerable<Account> accounts = getAccounts();

            //Remove once I work out how to get roles
           
            HashHelper hashHelper = new HashHelper();

            Account foundAccount = accounts.FirstOrDefault(u => u.LoginEmail.Equals(email) 
                && u.Password.Equals(hashHelper.CalculateHash(clearTextPassword, u.LoginEmail)));

            if ( foundAccount == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
             
 
            return new User("", roles );
        }
 
    }

}
