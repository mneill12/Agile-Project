﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    // Interface for custom methods of StoryTaskRepository
    public interface IStoryTaskRepository : IDataRepository<StoryTask>
    {
        IEnumerable<StoryTask> GetTasksByOwner(Account owner);
        IEnumerable<StoryTask> GetBlockedTasks();
        IEnumerable<StoryTask> GetTasksByStatus(CurrentStatus status);

    }
}
