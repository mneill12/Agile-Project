using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // BurndownPoint LINQ Entity Queries
    [Export(typeof (ITaskBurndownPointRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TaskBurndownPointReository : DataRepositoryBase<TaskBurndownPoint>, ITaskBurndownPointRepository
    {
        protected override TaskBurndownPoint AddEntity(Csc3045AgileContext entityContext, TaskBurndownPoint entity)
        {
            return entityContext.TaskBurndownPointSet.Add(entity);
        }

        protected override TaskBurndownPoint UpdateEntity(Csc3045AgileContext entityContext, TaskBurndownPoint entity)
        {
            return (from e in entityContext.TaskBurndownPointSet
                    where e.TaskBurndownPointId == entity.TaskBurndownPointId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TaskBurndownPoint> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.TaskBurndownPointSet
                   select e;
        }

        protected override TaskBurndownPoint GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.TaskBurndownPointSet
                         where e.TaskBurndownPointId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TaskBurndownPoint> GetTaskBurndownPointsByStoryTaskId(int storyTaskID)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                var query = (from tbp in entityContext.TaskBurndownPointSet
                    where tbp.StoryTaskId == storyTaskID
                    select tbp);

                return query.ToList();
            }
        }


        public IEnumerable<Dictionary<DateTime, int>> GetTotalHoursRemainingByStoryId(int storyTaskId)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                var query = (from tbp in entityContext.TaskBurndownPointSet
                    where tbp.StoryTaskId == storyTaskId
                    select tbp.HoursRemaining);

                return query;
            }
        }


        public IEnumerable<Dictionary<DateTime, int>> GetTotalHoursRemainingByDay(DateTime date)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                var query = (from tbp in entityContext.TaskBurndownPointSet
                                 where tbp.HoursRemaining.ContainsKey(date)
                                 select tbp.HoursRemaining);

                return query;
            }
        }


    }

}


