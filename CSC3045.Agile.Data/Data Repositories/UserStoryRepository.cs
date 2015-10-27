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
    // UserStory LINQ Entity Queries
    public class UserStoryRepository : DataRepositoryBase<UserStory>
    {
        protected override UserStory AddEntity(CSC3045AgileContext entityContext, UserStory entity)
        {
            return entityContext.UserStorySet.Add(entity);
        }

        protected override UserStory UpdateEntity(CSC3045AgileContext entityContext, UserStory entity)
        {
            return (from e in entityContext.UserStorySet
                    where e.UserStoryId == entity.UserStoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserStory> GetEntities(CSC3045AgileContext entityContext)
        {
            return from e in entityContext.UserStorySet
                   select e;
        }

        protected override UserStory GetEntity(CSC3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.UserStorySet
                         where e.UserStoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
