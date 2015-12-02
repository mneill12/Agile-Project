using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;
using Core.Common.Contracts;
using System.Collections.Generic;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of SprintRepository
    public interface ISprintRepository : IDataRepository<Sprint>
    {
        ICollection<Sprint> GetSprintForProject(int projectSprintId);
        ICollection<Sprint> GetSprintForScrumMaster(int scrumMasterId);
        ICollection<Sprint> GetSprintForAccount(int accountId);
        Sprint AddSprintWithTeam(Sprint sprint);
        Sprint UpdateSprintWithTeam(Sprint sprint);

    }
}