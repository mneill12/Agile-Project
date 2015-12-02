using System.Collections.Generic;
using Core.Common.Exceptions;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using System.ServiceModel;
using Core.Common.Contracts;
using System.ComponentModel.Composition;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Business.Contracts;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class UserStoryService : ServiceBase, IUserStoryService
    {
        [Import]
        private IDataRepositoryFactory _DataRepositoryFactory;

        public UserStoryService()
        {
        }

        public UserStoryService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }



        public UserStory GetUserStoryById(int userStoryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserStoryRepository userStoryRepository = _DataRepositoryFactory.GetDataRepository<IUserStoryRepository>();

                UserStory userStoryEntity = userStoryRepository.GetUserStoryById(userStoryId);
                if (userStoryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("User story with ID:{userStoryId} is not in database", userStoryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userStoryEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateUserStoryById(UserStory userStory)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IUserStoryRepository userStoryRepository = _DataRepositoryFactory.GetDataRepository<IUserStoryRepository>();

                UserStory updatedUserStory = userStoryRepository.Update(userStory);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserStory AddNewUserStory(UserStory userStory)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userStoryRepository = _DataRepositoryFactory.GetDataRepository<IUserStoryRepository>();

                return userStoryRepository.Add(userStory);
            });
        }


        public ICollection<UserStory> GetAllUserStories()
        {
            var userStoryRepository = _DataRepositoryFactory.GetDataRepository<IUserStoryRepository>();

            return userStoryRepository.GetUserStories();
        }

        public ICollection<UserStory> GetAllStoriesForProject(int projectId)
        {
            var userStoryRepository = _DataRepositoryFactory.GetDataRepository<IUserStoryRepository>();

            return userStoryRepository.GetUserStoriesByProject(projectId);
        }
    }
}
