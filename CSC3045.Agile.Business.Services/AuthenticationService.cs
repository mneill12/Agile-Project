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
  
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;
      
        [OperationBehavior(TransactionScopeRequired = true)]
        public Account AuthenticateUser(string email, string clearTextPassword)
        {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            Account foundAccount = accountRepository.GetByLogin(email);
            HashHelper hashHelper = new HashHelper();
            Account account = new Account();

            if (foundAccount == null)
            {
                return foundAccount;
            }

            else if ((hashHelper.CalculateHash(clearTextPassword, email)).Equals(foundAccount.Password))
            {
                //Lovely work around for the the serivce failure.
                
                account.AccountId = foundAccount.AccountId;
                account.UserRoles = foundAccount.UserRoles;
                account.LoginEmail = foundAccount.LoginEmail;

                return account;
            }
            
            return account;
           
        }
 
    }

}
