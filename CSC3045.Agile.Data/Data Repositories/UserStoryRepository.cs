using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // UserStory LINQ Entity Queries
    [Export(typeof (IUserStoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserStoryRepository : DataRepositoryBase<UserStory>, IUserStoryRepository
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


        ICollection<UserStory> IUserStoryRepository.GetUserStories()
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.UserStorySet
                    .Include(s => s.Status)
                    .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                    .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria)).ToList();
            }
        }

        ICollection<UserStory> IUserStoryRepository.GetUserStoriesByStatus(CurrentStatus status)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.UserStorySet
                    .Include(s => s.Status)
                    .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                    .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria))
                    .Where(s => s.Status == status)
                    .ToList();
            }
        }

        public UserStory GerUserStoryById(int id)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.UserStorySet
                    .Include(s => s.Status)
                    .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                    .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria))
                    .FirstOrDefault(s => s.UserStoryId == id);
            }
        }
    }
}