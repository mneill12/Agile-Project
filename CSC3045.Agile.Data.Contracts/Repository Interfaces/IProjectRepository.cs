using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of ProjectRepository
    public interface IProjectRepository : IDataRepository<Project>
    {
        IEnumerable<Project> GetProjectsForProjectManager(int projectManagerId);
        IEnumerable<Project> GetProjectsForProductOwner(int productOwnderId);
        IEnumerable<Project> GetProjectsForAccount(int accountId);
    }
}