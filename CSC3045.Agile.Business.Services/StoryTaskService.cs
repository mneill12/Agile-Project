using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
    ConcurrencyMode = ConcurrencyMode.Multiple,
    ReleaseServiceInstanceOnTransactionComplete = false)]
    public class StoryTaskService : ServiceBase, IStoryTaskService
    {
        [Import]
        private IDataRepositoryFactory _DataRepositoryFactory;

        public StoryTaskService()
        {
        }

        /// <summary>
        /// Used for testing, this service has the data repository initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        public StoryTaskService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        

        public StoryTask CreateTask(StoryTask storyTask)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                return storyTaskRepository.Add(storyTask);
            });
        }

        public StoryTask GetTaskById(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                var storyTask = storyTaskRepository.Get(id);

                if (storyTask == null)
                {
                    var ex = new NotFoundException(string.Format("StoryTask with Id {0} could not be found", id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return storyTask;
            });
        }

        public StoryTask UpdateTask(StoryTask storyTask)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                var updatedStoryTask = storyTaskRepository.Update(storyTask);

                if (updatedStoryTask == null)
                {
                    var ex = new NotFoundException(string.Format("StoryTask {0} could not be updated or found", storyTask.Title));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return updatedStoryTask;
            });
        }

        public void DeleteTaskById(int id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                storyTaskRepository.Remove(id);
            });
        }

        public ICollection<StoryTask> GetAllTasks()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                ICollection<StoryTask> allStoryTasks = (ICollection<StoryTask>) storyTaskRepository.Get();

                if (allStoryTasks == null || allStoryTasks.Count == 0)
                {
                    var ex = new NotFoundException("Could not find any story tasks to get");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return allStoryTasks;
            });
        }

        public ICollection<StoryTask> GetTasksByOwner(int accountId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                ICollection<StoryTask> storyTasks = storyTaskRepository.GetTasksByOwner(accountId);

                if (storyTasks == null || storyTasks.Count == 0)
                {
                    var ex = new NotFoundException(string.Format("Could not find any story tasks for account Id {0}", accountId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return storyTasks;
            });
        }

        //TODO:Blocked tasks for sprint/project, these all have to be project specific as it states as a developer
        public ICollection<StoryTask> GetBlockedTasks()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                ICollection<StoryTask> storyTasks = storyTaskRepository.GetBlockedTasks();

                if (storyTasks == null || storyTasks.Count == 0)
                {
                    var ex = new NotFoundException("Could not find any blocked tasks");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return storyTasks;
            });
        }

        public ICollection<StoryTask> GetTasksByStatus(string status)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                ICollection<StoryTask> storyTasks = storyTaskRepository.GetTasksByStatus(status);

                if (storyTasks == null || storyTasks.Count == 0)
                {
                    var ex = new NotFoundException(string.Format("Could not find any story tasks with status: {0}", status));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return storyTasks;
            });
        }

        public ICollection<StoryTask> UpdateTaskCollection(ICollection<StoryTask> updatedTasks)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var storyTaskRepository = _DataRepositoryFactory.GetDataRepository<IStoryTaskRepository>();

                ICollection<StoryTask> updatedStoryTasks = storyTaskRepository.UpdateTaskCollection(updatedTasks);

                if (updatedStoryTasks == null || updatedStoryTasks.Count < updatedTasks.Count)
                {
                    var ex = new NotFoundException("There was a problem updating the collection of tasks");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return updatedStoryTasks;
            });
        }
    }
}
