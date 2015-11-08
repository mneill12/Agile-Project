using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // BurndownPoint LINQ Entity Queries
    [Export(typeof (IBurndownPointRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BurndownPointRepository : DataRepositoryBase<BurndownPoint>
    {
        protected override BurndownPoint AddEntity(Csc3045AgileContext entityContext, BurndownPoint entity)
        {
            return entityContext.BurndownPointSet.Add(entity);
        }

        protected override BurndownPoint UpdateEntity(Csc3045AgileContext entityContext, BurndownPoint entity)
        {
            return (from e in entityContext.BurndownPointSet
                where e.BurndownPointId == entity.BurndownPointId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<BurndownPoint> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.BurndownPointSet
                select e;
        }

        protected override BurndownPoint GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.BurndownPointSet
                where e.BurndownPointId == id
                select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}