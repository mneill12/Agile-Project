using System.Collections.Generic;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IStoryTaskService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        StoryTask CreateTask(StoryTask storyTask);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        StoryTask GetTaskById(int id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        StoryTask UpdateTask(StoryTask storyTask);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTaskById(int id);

        //Custom 

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<StoryTask> GetAllTasks();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<StoryTask> GetTasksByOwner(int accountId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<StoryTask> GetBlockedTasks();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<StoryTask> GetTasksByStatus(string status);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ICollection<StoryTask> UpdateTaskCollection(ICollection<StoryTask> updatedTasks);

    }
}