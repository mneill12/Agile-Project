using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // StoryStatus LINQ Entity Queries
    [Export(typeof (IStoryStatusRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StoryStatusRepository : DataRepositoryBase<CurrentStatus>
    {
        protected override CurrentStatus AddEntity(Csc3045AgileContext entityContext, CurrentStatus entity)
        {
            return entityContext.CurrentStatusSet.Add(entity);
        }

        protected override CurrentStatus UpdateEntity(Csc3045AgileContext entityContext, CurrentStatus entity)
        {
            return (from e in entityContext.CurrentStatusSet
                    where e.CurrentStatusId == entity.CurrentStatusId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<CurrentStatus> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.CurrentStatusSet
                   select e;
        }

        protected override CurrentStatus GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.CurrentStatusSet
                         where e.CurrentStatusId == id
                select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}