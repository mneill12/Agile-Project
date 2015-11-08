using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // PlanningPokerSession LINQ Entity Queries
    [Export(typeof (IPlanningPokerSessionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlanningPokerSessionRepository : DataRepositoryBase<PlanningPokerSession>
    {
        protected override PlanningPokerSession AddEntity(Csc3045AgileContext entityContext, PlanningPokerSession entity)
        {
            return entityContext.PlanningPokerSessionSet.Add(entity);
        }

        protected override PlanningPokerSession UpdateEntity(Csc3045AgileContext entityContext,
            PlanningPokerSession entity)
        {
            return (from e in entityContext.PlanningPokerSessionSet
                where e.PlanningPokerSessionId == entity.PlanningPokerSessionId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<PlanningPokerSession> GetEntities(Csc3045AgileContext entityContext)
        {
            return entityContext.PlanningPokerSessionSet
                .Include(a => a.InvitedAccountSet)
                .Include(b => b.UserStories.Select(c => c.Status))
                .ToList();
        }

        protected override PlanningPokerSession GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.PlanningPokerSessionSet
                .Include(a => a.InvitedAccountSet)
                .Include(b => b.UserStories.Select(c => c.Status))
                .FirstOrDefault();
        }
    }
}