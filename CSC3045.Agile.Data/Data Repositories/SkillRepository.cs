using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Data.Data_Repositories
{
    // Skill LINQ Entity Queries
    [Export(typeof(ISkillRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SkillRepository : DataRepositoryBase<Skill>, ISkillRepository
    {
        public IList<Skill> GetAllSkills()
        {
            using (var entityContext = new Csc3045AgileContext())
            {
                return entityContext.SkillSet.ToList();
            }
        }

        protected override Skill AddEntity(Csc3045AgileContext entityContext, Skill entity)
        {
            return entityContext.SkillSet.Add(entity);
        }

        protected override Skill UpdateEntity(Csc3045AgileContext entityContext, Skill entity)
        {
            return (from e in entityContext.SkillSet
                    where e.SkillId == entity.SkillId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Skill> GetEntities(Csc3045AgileContext entityContext)
        {
            return from e in entityContext.SkillSet
                   select e;
        }

        protected override Skill GetEntity(Csc3045AgileContext entityContext, int id)
        {
            var query = (from e in entityContext.SkillSet
                         where e.SkillId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}