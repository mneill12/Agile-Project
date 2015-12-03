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

        public ICollection<Sprint> GetSprintsForProjectId(int projectId)
        {
            return Channel.GetSprintsForProjectId(projectId);
        }

        public ICollection<Sprint> GetSprintsForAccountId(int accountId)
        {
            return Channel.GetSprintsForAccountId(accountId);
        }
    }
}