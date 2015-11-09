using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of StoryTaskRepository
    public interface IStoryTaskRepository : IDataRepository<StoryTask>
    {
        IEnumerable<StoryTask> GetTasksByOwner(int ownerId);
        IEnumerable<StoryTask> GetBlockedTasks();
        IEnumerable<StoryTask> GetTasksByStatus(string status);
    }
}