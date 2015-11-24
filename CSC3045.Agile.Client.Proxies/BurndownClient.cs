using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Proxies
{
    [Export( typeof(IBurndownService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class BurndownClient :ClientBase<IBurndownService>, IBurndownService
    {

        public IEnumerable<TaskBurndownPoint> GetTaskBurndownPointsForStoryTask(int storyTaskId)
        {
            return Channel.GetTaskBurndownPointsForStoryTask(storyTaskId);
        }

        public int GetTotalHoursRemainingForStory(int storyId)
        {
            return Channel.GetTotalHoursRemainingForStory(storyId);
        }

        public int GetTotalHoursRemainingForDay(System.DateTime date)
        {
            return Channel.GetTotalHoursRemainingForDay(date);
        }

        public int GetOrigionalTotalHoursForSprintId(int sprintId)
        {
            return Channel.GetOrigionalTotalHoursForSprintId(sprintId);
        }

        public Dictionary<DateTime, int> GetHourDateMapForSprintId(int sprintId)
        {
            return Channel.GetHourDateMapForSprintId(sprintId);
        }
    }
}
