using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Common.Contracts
{
    // Get a data repository
    public interface IDataRepositoryFactory
    {
        T GetDataRepository<T>() where T : IDataRepository;
    }
}
