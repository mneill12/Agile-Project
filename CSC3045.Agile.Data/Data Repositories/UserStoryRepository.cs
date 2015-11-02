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
using Core.Common.Core;
using System.Data.Entity;
using CSC3045.Agile.Data;

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
                return entityContext.UserStorySet
                    .Include(s => s.Status)
                    .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                    .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria)).ToList();
        }

        protected override UserStory GetEntity(Csc3045AgileContext entityContext, int id)
        {
                return entityContext.UserStorySet
                    .Include(s => s.Status)
                    .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                    .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria))
                    .FirstOrDefault(s => s.UserStoryId == id);
        }
    }
}
