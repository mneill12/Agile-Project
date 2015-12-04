using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Sprint LINQ Entity Queries
    [Export(typeof (ISprintRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SprintRepository : DataRepositoryBase<Sprint>, ISprintRepository
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
                .ToList();
        }

        protected override Sprint GetEntity(Csc3045AgileContext entityContext, int id)
        {
            return entityContext.SprintSet
                .Include(a => a.SprintMembers.Select(b => b.UserRoles))
                .FirstOrDefault();
        }

        public ICollection<Sprint> GetSprintForProject(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Sprint> GetSprintForScrumMaster(int scrumMasterId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Sprint> GetSprintForAccount(int accountId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Sprint> GetSprintStartDate(int sprintId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Sprint> GetSprintEndDate(int sprintId)
        {
            throw new System.NotImplementedException();
        }

        public Sprint AddSprintWithTeam(Sprint sprint)
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                // You're not wrong....
                sprint.SprintName = sprint.SprintName;
                sprint.SprintId = sprint.SprintId;
                sprint.EndDate = sprint.EndDate;
                sprint.StartDate = sprint.StartDate;
                sprint.SprintNumber = sprint.SprintNumber;

                sprint.ScrumMaster = entityContext.AccountSet.Single(a => a.AccountId == sprint.ScrumMaster.AccountId);
                sprint.SprintMembers = sprint.SprintMembers.Select(user => entityContext.AccountSet.Single(a => a.AccountId == user.AccountId)).ToList();

                Sprint addedSprint = AddEntity(entityContext, sprint);
                entityContext.SaveChanges();

                return addedSprint;
            }
        }

        public Sprint UpdateSprintWithTeam(Sprint sprint)
        {
            throw new System.NotImplementedException();
        }
    }
}