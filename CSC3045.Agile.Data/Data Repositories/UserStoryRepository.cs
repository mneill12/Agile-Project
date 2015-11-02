using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Data;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using CSC3045.Agile.Data;
using System.Data.Entity;


namespace CSC3045.Agile.Data.Data_Repositories
{
    // UserStory LINQ Entity Queries
    public class UserStoryRepository : DataRepositoryBase<UserStory>
    {
      
        protected override UserStory AddEntity(Csc3045AgileContext entityContext, UserStory entity)
        {
            return entityContext.UserStorySet.Add(entity);
        }

        protected override UserStory UpdateEntity(Csc3045AgileContext entityContext, UserStory entity)
        {
            return (from e in entityContext.UserStorySet
                    where e.UserStoryId == entity.UserStoryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<UserStory> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.UserStorySet
                   select e;
        }

        protected override UserStory GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.UserStorySet
                         where e.UserStoryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected IEnumerable<UserStory> GetEntitiesWithChildren(Csc3045AgileContext entityContext)
        {
            return entityContext.UserStorySet
                .Include(s => s.Status)
                .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria)).ToList();
        }

        protected UserStory GetEntityWithChildren(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.UserStorySet
                .Include(s => s.Status)
                .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria))
                .FirstOrDefault(s => s.UserStoryId == id);
        }
    }
}
