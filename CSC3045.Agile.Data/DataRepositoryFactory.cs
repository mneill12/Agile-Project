using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Data.Contracts
{
    // Get class within DI container that is associated with that repository using MEF.
    // Gets and exports data repository through MEF when given any interface of a repository.
    [Export(typeof (IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        #region IDataRepositoryFactory Members

        T IDataRepositoryFactory.GetDataRepository<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }

        #endregion
    }
}