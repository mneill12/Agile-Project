using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business
{
    // Allows DI for business engines in services when we have many engine to limit instantiation
    [Export(typeof (IBusinessEngineFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        T IBusinessEngineFactory.GetBusinessEngine<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}