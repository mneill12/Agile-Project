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
    // AcceptanceCriteria LINQ Entity Queries
    public class AcceptanceCriteriaRepository : DataRepositoryBase<AcceptanceCriteria>
    {
        protected override AcceptanceCriteria AddEntity(CSC3045AgileContext entityContext, AcceptanceCriteria entity)
        {
            return entityContext.AcceptanceCriteriaSet.Add(entity);
        }

        protected override AcceptanceCriteria UpdateEntity(CSC3045AgileContext entityContext, AcceptanceCriteria entity)
        {
            return (from e in entityContext.AcceptanceCriteriaSet
                where e.AcceptanceCriteriaId == entity.AcceptanceCriteriaId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<AcceptanceCriteria> GetEntities(CSC3045AgileContext entityContext)
        {
            return from e in entityContext.AcceptanceCriteriaSet
                   select e;
        }

        protected override AcceptanceCriteria GetEntity(CSC3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.AcceptanceCriteriaSet
                         where e.AcceptanceCriteriaId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
