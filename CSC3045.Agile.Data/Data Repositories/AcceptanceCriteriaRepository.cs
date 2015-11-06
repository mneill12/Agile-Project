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
using System.Data.Entity;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // AcceptanceCriteria LINQ Entity Queries
    [Export(typeof(IAcceptanceCriteriaRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AcceptanceCriteriaRepository : DataRepositoryBase<AcceptanceCriteria>
    {
        protected override AcceptanceCriteria AddEntity(Csc3045AgileContext entityContext, AcceptanceCriteria entity)
        {
            return entityContext.AcceptanceCriteriaSet.Add(entity);
        }

        protected override AcceptanceCriteria UpdateEntity(Csc3045AgileContext entityContext, AcceptanceCriteria entity)
        {
            return (from e in entityContext.AcceptanceCriteriaSet
                where e.AcceptanceCriteriaId == entity.AcceptanceCriteriaId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<AcceptanceCriteria> GetEntities(Csc3045AgileContext entityContext)
        {
            return entityContext.AcceptanceCriteriaSet
                .Include(a => a.Criteria)
                .ToList();
        }

        protected override AcceptanceCriteria GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.AcceptanceCriteriaSet
                .Include(a => a.Criteria)
                .FirstOrDefault();
        }
 
    }
}
