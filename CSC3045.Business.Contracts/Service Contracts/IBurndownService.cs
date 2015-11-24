using System;
using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IBurndownService
    {

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IEnumerable<TaskBurndownPoint> GetTaskBurndownPointsForStoryTask(int storyTaskId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        int GetTotalHoursRemainingForStory(int storyId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        int GetTotalHoursRemainingForDay(DateTime date);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        int GetOrigionalTotalHoursForSprintId(int sprintId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Dictionary<DateTime, int> GetHourDateMapForSprintId(int sprintId);

    }
}
