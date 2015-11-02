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
    // PlanningPokerStory LINQ Entity Queries
    public class PlanningPokerStoryRepository : DataRepositoryBase<PlanningPokerStory>
    {
        protected override PlanningPokerStory AddEntity(Csc3045AgileContext entityContext, PlanningPokerStory entity)
        {
            return entityContext.PlanningPokerStorySet.Add(entity);
        }

        protected override PlanningPokerStory UpdateEntity(Csc3045AgileContext entityContext, PlanningPokerStory entity)
        {
            return (from e in entityContext.PlanningPokerStorySet
                    where e.PlanningPokerStoryId == entity.PlanningPokerStoryId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<PlanningPokerStory> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.PlanningPokerStorySet
                   select e;
        }

        protected override PlanningPokerStory GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.PlanningPokerStorySet
                         where e.PlanningPokerStoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
