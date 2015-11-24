using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IBurndownService : IServiceContract
    {
        [OperationContract]
        IEnumerable<TaskBurndownPoint> GetTaskBurndownPointsForStoryTask(int storyTaskId);

        [OperationContract]
        int GetTotalHoursRemainingForStory(int storyId);

        [OperationContract]
        int GetTotalHoursRemainingForDay(DateTime date);

        [OperationContract]
        int GetOrigionalTotalHoursForSprintId(int sprintId);

        [OperationContract]
        Dictionary<DateTime, int> GetHourDateMapForSprintId(int sprintId);



    }
}
