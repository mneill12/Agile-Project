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
    // StoryStatus LINQ Entity Queries
    public class StoryStatusRepository : DataRepositoryBase<CurrentStatus>
    {
        protected override CurrentStatus AddEntity(Csc3045AgileContext entityContext, CurrentStatus entity)
        {
            return entityContext.StoryStatusSet.Add(entity);
        }

        protected override CurrentStatus UpdateEntity(Csc3045AgileContext entityContext, CurrentStatus entity)
        {
            return (from e in entityContext.StoryStatusSet
                    where e.StoryStatusId == entity.StoryStatusId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CurrentStatus> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.StoryStatusSet
                   select e;
        }

        protected override CurrentStatus GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.StoryStatusSet
                         where e.StoryStatusId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
