using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof(IStoryTaskService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class StoryTaskClient : ClientBase<IStoryTaskService>, IStoryTaskService
    {
        public StoryTask CreateTask(StoryTask storyTask)
        {
            return Channel.CreateTask(storyTask);
        }

        public StoryTask GetTaskById(int id)
        {
            return Channel.GetTaskById(id);
        }

        public StoryTask UpdateTask(StoryTask storyTask)
        {
            return Channel.UpdateTask(storyTask);
        }

        public void DeleteTaskById(int id)
        {
            Channel.DeleteTaskById(id);
        }

        public ICollection<StoryTask> GetAllTasks()
        {
            return Channel.GetAllTasks();
        }

        public ICollection<StoryTask> GetTasksByOwner(int accountId)
        {
            return Channel.GetTasksByOwner(accountId);
        }

        public ICollection<StoryTask> GetBlockedTasks()
        {
            return Channel.GetBlockedTasks();
        }

        public ICollection<StoryTask> GetTasksByStatus(string status)
        {
            return Channel.GetTasksByStatus(status);
        }

        public ICollection<StoryTask> UpdateTaskCollection(ICollection<StoryTask> updatedTasks)
        {
            return Channel.UpdateTaskCollection(updatedTasks);
        }
    }
}
