using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Burndown LINQ Entity Queries
    [Export(typeof (IBurndownRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BurndownRepository : DataRepositoryBase<Burndown>
    {
        protected override Burndown AddEntity(Csc3045AgileContext entityContext, Burndown entity)
        {
            return entityContext.BurndownSet.Add(entity);
        }

        protected override Burndown UpdateEntity(Csc3045AgileContext entityContext, Burndown entity)
        {
            return (from e in entityContext.BurndownSet
                where e.BurndownId == entity.BurndownId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<Burndown> GetEntities(Csc3045AgileContext entityContext)
        {
            return entityContext.BurndownSet
                .Include(a => a.BurndownPoints)
                .ToList();
        }

        protected override Burndown GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.BurndownSet
                .Include(a => a.BurndownPoints)
                .FirstOrDefault();
        }
    }
}