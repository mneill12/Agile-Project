using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Data;

namespace CSC3045.Agile.Data
{
    // Allows the base class DataRepositoryBase to instantiate DbContext with overridable methods. Used by data repositories.
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, CSC3045AgileContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
