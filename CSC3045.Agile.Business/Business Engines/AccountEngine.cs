using System.ComponentModel.Composition;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Common;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Business_Engines
{
    [Export(typeof (IAccountEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountEngine : IAccountEngine
    {
        private readonly IDataRepositoryFactory _DataRepositoryFactory;

        [ImportingConstructor]
        public AccountEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public bool IsAccountAlreadyCreated(string loginEmail)
        {
            var exists = false;

            var accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            if (accountRepository.GetByLogin(loginEmail) != null)
                exists = true;

            return exists;
        }
    }
}