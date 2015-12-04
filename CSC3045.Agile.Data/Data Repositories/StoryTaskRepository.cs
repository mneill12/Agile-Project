using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // StoryTask LINQ Entity Queries
    [Export(typeof (IStoryTaskRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StoryTaskRepository : DataRepositoryBase<StoryTask>, IStoryTaskRepository
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
                .FirstOrDefault(t => t.StoryTaskId == id);
        }

        // Get StoryTasks by Owner
        public ICollection<StoryTask> GetTasksByOwner(int accountId)
        {
            using (var entityContext = new Csc3045AgileContext())
            return entityContext.StoryTaskSet
                .Include(a => a.Owner)
                .Where(a => a.Owner.AccountId == accountId)
                .ToList();
        }

        // Get blocked tasks
        public ICollection<StoryTask> GetBlockedTasks()
        {
            using (var entityContext = new Csc3045AgileContext())
            return entityContext.StoryTaskSet
                .Include(a => a.Owner)
                .Where(a => a.IsBlocked)
                .ToList();
        }

        // Get tasks by status
        public ICollection<StoryTask> GetTasksByStatus(string status)
        {
            using (var entityContext = new Csc3045AgileContext())
            return entityContext.StoryTaskSet
               .Include(a => a.Owner)
               .Where(a => a.CurrentStatus.CurrentStatusName.Equals(status))
               .ToList();

        }

        public ICollection<StoryTask> UpdateTaskCollection(ICollection<StoryTask> updatedTasks)
        {
            ICollection<StoryTask> updatedStoryTasks = new List<StoryTask>();

            using (var entityContext = new Csc3045AgileContext())
            {
                foreach (var updatedTask in updatedTasks)
                {
                    updatedStoryTasks.Add(UpdateEntity(entityContext, updatedTask));
                }
            }

            return updatedStoryTasks;
        }

        public StoryTask UpdateOwnerShip(int id, int accountId)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                var storyTask = entityContext.StoryTaskSet
                .Include(a => a.Owner)
                .FirstOrDefault(t => t.StoryTaskId == id);

                if (storyTask != null && storyTask.Owner == null)
                {
                    var account = entityContext.AccountSet
                    .FirstOrDefault(a => a.AccountId == accountId);

                    storyTask.Owner = account;

                    entityContext.SaveChanges();
                    return storyTask;
                }

                return null;
            }

        }
    }
}