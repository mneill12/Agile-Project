using System.ComponentModel.Composition;
using System.ServiceModel;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        [Import] 
        private IDataRepositoryFactory _DataRepositoryFactory;
        [Import]
        private IBusinessEngineFactory _BusinessEngineFactory;

        [OperationBehavior(TransactionScopeRequired = true)]
        public Account AuthenticateUser(string email, string hashedPassword)
        {
            return
                _DataRepositoryFactory.GetDataRepository<IAccountRepository>()
                    .GetByLoginAndPasswordWithUserRoles(email, hashedPassword);
        }
        
        /// <summary>
        /// Used for testing, this service has the data repository initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        public AuthenticationService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public AuthenticationService()
        {
          
        }
    }

  
}