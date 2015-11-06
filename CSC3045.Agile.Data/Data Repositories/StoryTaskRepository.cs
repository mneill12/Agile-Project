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
using System.Data.Entity;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // StoryTask LINQ Entity Queries
    [Export(typeof(IStoryTaskRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
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
            return entityContext.StoryTaskSet
                .Include(a => a.Owner)
                .ToList();
        }

        protected override StoryTask GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.StoryTaskSet
               .Include(a => a.Owner)
               .FirstOrDefault();
        }

        // Get StoryTasks by Owner
        protected IEnumerable<StoryTask> GetTasksByOwner(Csc3045AgileContext entityContext, Account owner)
        {
            return entityContext.StoryTaskSet
               .Include(a => a.Owner)
               .Where(a => a.Owner.AccountId == owner.AccountId)
               .ToList();
        } 

        // Get blocked tasks
        protected IEnumerable<StoryTask> GetBlockedTasks(Csc3045AgileContext entityContext)
        {
            return entityContext.StoryTaskSet
               .Include(a => a.Owner)
               .Where(a => a.IsBlocked)
               .ToList();
        } 

        // Get tasks by status
        protected IEnumerable<StoryTask> GetTasksByStatus(Csc3045AgileContext entityContext, CurrentStatus status)
        {
            return entityContext.StoryTaskSet
               .Include(a => a.Owner)
               .Where(a => a.CurrentStatus.StoryStatusName.Equals(status.StoryStatusName))
               .ToList();
        } 
    }
}
