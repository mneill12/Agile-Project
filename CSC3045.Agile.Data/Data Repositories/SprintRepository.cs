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
    // Sprint LINQ Entity Queries
    public class SprintRepository : DataRepositoryBase<Sprint>
    {
        protected override Sprint AddEntity(CSC3045AgileContext entityContext, Sprint entity)
        {
            return entityContext.SprintSet.Add(entity);
        }

        protected override Sprint UpdateEntity(CSC3045AgileContext entityContext, Sprint entity)
        {
            return (from e in entityContext.SprintSet
                    where e.SprintId == entity.SprintId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Sprint> GetEntities(CSC3045AgileContext entityContext)
        {
            return from e in entityContext.SprintSet
                   select e;
        }

        protected override Sprint GetEntity(CSC3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.SprintSet
                         where e.SprintId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
