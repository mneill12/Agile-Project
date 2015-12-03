using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof(ISprintService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SprintClient : ClientBase<ISprintService>, ISprintService
    {
        public ICollection<Sprint> GetSprintForProject(int projectId)
        {
            return Channel.GetSprintForProject(projectId);
        }

        public Sprint GetSprintInfo(int sprintId){
            return Channel.GetSprintInfo(sprintId);
        }
       
        
        public Sprint AddSprint(Sprint sprint)
        {
            return Channel.AddSprint(sprint);
        }

        public void UpdateSprintInfo(Sprint sprint)
        {
            Channel.UpdateSprintInfo(sprint);
        }

        public ICollection<Sprint> GetAllSprints()
        {
            return Channel.GetAllSprints();
        }
    }
}