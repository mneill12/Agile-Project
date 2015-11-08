﻿using System;
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
               .FirstOrDefault();
        }

        // Get StoryTasks by Owner
        public IEnumerable<StoryTask> GetTasksByOwner(Account owner)
        {
            using (var entityContext = new Csc3045AgileContext())
            return entityContext.StoryTaskSet
               .Include(a => a.Owner)
               .Where(a => a.Owner.AccountId == owner.AccountId)
               .ToList();
        } 

        // Get blocked tasks
        public IEnumerable<StoryTask> GetBlockedTasks()
        {
            using (var entityContext = new Csc3045AgileContext())
            return entityContext.StoryTaskSet
               .Include(a => a.Owner)
               .Where(a => a.IsBlocked)
               .ToList();
        } 

        // Get tasks by status
        public IEnumerable<StoryTask> GetTasksByStatus(CurrentStatus status)
        {
            using (var entityContext = new Csc3045AgileContext())
            return entityContext.StoryTaskSet
               .Include(a => a.Owner)
               .Where(a => a.CurrentStatus.StoryStatusName.Equals(status.StoryStatusName))
               .ToList();

        } 
    }
}
