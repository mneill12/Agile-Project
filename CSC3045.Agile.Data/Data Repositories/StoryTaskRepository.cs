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
    // StoryTask LINQ Entity Queries
    public class StoryTaskRepository : DataRepositoryBase<StoryTask>
    {
        protected override StoryTask AddEntity(Csc3045AgileContext entityContext, StoryTask entity)
        {
            return entityContext.StoryTaskSet.Add(entity);
        }

        protected override StoryTask UpdateEntity(Csc3045AgileContext entityContext, StoryTask entity)
        {
            return (from e in entityContext.StoryTaskSet
                    where e.StoryTaskId == entity.StoryTaskId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<StoryTask> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.StoryTaskSet
                   select e;
        }

        protected override StoryTask GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.StoryTaskSet
                         where e.StoryTaskId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
