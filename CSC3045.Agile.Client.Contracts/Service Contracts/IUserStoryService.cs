using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IUserStoryService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserStory GetUserStoryById(int userStoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<UserStory> GetAllUserStories();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateUserStoryById(UserStory userStory);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserStory AddNewUserStory(UserStory userStory);

        [OperationContract]
        ICollection<UserStory> GetAllStoriesForProject(int projectId);
    }
}
