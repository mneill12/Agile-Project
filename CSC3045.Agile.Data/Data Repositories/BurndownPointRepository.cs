using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Data;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // BurndownPoint LINQ Entity Queries
    public class BurndownPointRepository : DataRepositoryBase<BurndownPoint>
    {
        protected override BurndownPoint AddEntity(CSC3045AgileContext entityContext, BurndownPoint entity)
        {
            return entityContext.BurndownPointSet.Add(entity);
        }

        protected override BurndownPoint UpdateEntity(CSC3045AgileContext entityContext, BurndownPoint entity)
        {
            return (from e in entityContext.BurndownPointSet
                    where e.BurndownPointId == entity.BurndownPointId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<BurndownPoint> GetEntities(CSC3045AgileContext entityContext)
        {
            return from e in entityContext.BurndownPointSet
                   select e;
        }

        protected override BurndownPoint GetEntity(CSC3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.BurndownPointSet
                         where e.BurndownPointId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
