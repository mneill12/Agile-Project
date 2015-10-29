using System;
using System.ComponentModel.Composition;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Common;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Business_Engines
{
    [Export(typeof(IAccountEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountEngine : IAccountEngine
    {
        [ImportingConstructor]
        public AccountEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        IDataRepositoryFactory _DataRepositoryFactory;

        public bool IsAccountAlreadyCreated(string loginEmail)
        {
            bool exists = false;

            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            if (accountRepository != null)
                exists = true;

            return exists;
        }
    }
}
