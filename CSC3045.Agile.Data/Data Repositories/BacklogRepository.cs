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
using System.Data.Entity;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Backlog LINQ Entity Queries
    public class BacklogRepository : DataRepositoryBase<Backlog>
    {
        protected override Backlog AddEntity(Csc3045AgileContext entityContext, Backlog entity)
        {
            return entityContext.BacklogSet.Add(entity);
        }

        protected override Backlog UpdateEntity(Csc3045AgileContext entityContext, Backlog entity)
        {
            return (from e in entityContext.BacklogSet
                where e.BacklogId == entity.BacklogId
                select e).FirstOrDefault();
        }

        protected override IEnumerable<Backlog> GetEntities(Csc3045AgileContext entityContext)
        {
            return entityContext.BacklogSet
                 .Include(a => a.AssociatedUserStories)
                 .ToList();
        }

        protected override Backlog GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.BacklogSet
                .Include(a => a.AssociatedUserStories)
                .FirstOrDefault();
        }
    }
}
