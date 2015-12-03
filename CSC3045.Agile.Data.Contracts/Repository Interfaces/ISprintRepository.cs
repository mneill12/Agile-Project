using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;
using Core.Common.Contracts;
using System.Collections.Generic;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of SprintRepository
    public interface ISprintRepository : IDataRepository<Sprint>
    {
        ICollection<Sprint> GetSprintForProject(int projectId);
        ICollection<Sprint> GetSprintForScrumMaster(int scrumMasterId);
        ICollection<Sprint> GetSprintForAccount(int accountId);
        ICollection<Sprint> GetSprintStartDate(int sprintId);
        ICollection<Sprint> GetSprintEndDate(int sprintId);
        Sprint AddSprintWithTeam(Sprint sprint);
        Sprint UpdateSprintWithTeam(Sprint sprint);

    }
}