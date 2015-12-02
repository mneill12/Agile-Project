using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Core.Common.Exceptions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IUserStoryService
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
        ICollection<UserStory> GetAllStoriesForProject(int projectId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserStory AddNewUserStory(UserStory userStory);
    }
}
