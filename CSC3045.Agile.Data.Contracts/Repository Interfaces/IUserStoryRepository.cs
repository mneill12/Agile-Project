using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of UserStoryRepository
    public interface IUserStoryRepository : IDataRepository<UserStory>
    {
        ICollection<UserStory> GetUserStories();
        ICollection<UserStory> GetUserStoriesByStatus(CurrentStatus status);
        ICollection<UserStory> GetUserStoriesByProject(int projectId);
        UserStory GetUserStoryById(int id);
    }
}