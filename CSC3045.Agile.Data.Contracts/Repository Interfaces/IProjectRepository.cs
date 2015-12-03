using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of ProjectRepository
    public interface IProjectRepository : IDataRepository<Project>
    {
        ICollection<Project> GetProjectsForProjectManager(int projectManagerId);
        ICollection<Project> GetProjectsForProductOwner(int productOwnderId);
        ICollection<Project> GetProjectsForAccount(int accountId);
        Project AddBacklogStoryToProject(int projectId);
        Project AddProjectWithUsers(Project project);
        Project UpdateProjectWithUsers(Project project);
    }
}