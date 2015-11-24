using System;
using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data.Contracts.Repository_Interfaces
{
    public interface ITaskBurndownPointRepository : IDataRepository<TaskBurndownPoint>
    {
        IEnumerable<TaskBurndownPoint> GetTaskBurndownPointsByStoryTaskId(int storyTaskId);
        IEnumerable<Dictionary<DateTime, int>> GetTotalHoursRemainingByStoryId(int storyTaskId);
        IEnumerable<Dictionary<DateTime, int>> GetTotalHoursRemainingByDay(DateTime date);
    }
}
