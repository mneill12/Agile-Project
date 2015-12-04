using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of StoryTaskRepository
    public interface IStoryTaskRepository : IDataRepository<StoryTask>
    {
        ICollection<StoryTask> GetTasksByOwner(int ownerId);
        ICollection<StoryTask> GetBlockedTasks();
        ICollection<StoryTask> GetTasksByStatus(string status);
        ICollection<StoryTask> UpdateTaskCollection(ICollection<StoryTask> updatedTasks);
        StoryTask UpdateOwnerShip(int id, int accountId);
    }
}