﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Sprint LINQ Entity Queries
    [Export(typeof (ISprintRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SprintRepository : DataRepositoryBase<Sprint>
    {
        protected override Sprint AddEntity(Csc3045AgileContext entityContext, Sprint entity)
        {
            return entityContext.SprintSet.Add(entity);
        }

        protected override Sprint UpdateEntity(Csc3045AgileContext entityContext, Sprint entity)
        {
            return (from e in entityContext.SprintSet
                where e.SprintId == entity.SprintId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<Sprint> GetEntities(Csc3045AgileContext entityContext)
        {
            return entityContext.SprintSet
                .Include(a => a.SprintMembers.Select(b => b.UserRoles))
                .Include(c => c.Burndowns.Select(d => d.BurndownPoints))
                .ToList();
        }

        protected override Sprint GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.SprintSet
                .Include(a => a.SprintMembers.Select(b => b.UserRoles))
                .Include(c => c.Burndowns.Select(d => d.BurndownPoints))
                .FirstOrDefault();
        }
    }
}