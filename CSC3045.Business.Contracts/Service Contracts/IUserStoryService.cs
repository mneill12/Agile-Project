using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IUserStoryService
    {
        //CRUD

        //Create
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserStory AddNewUserStory(UserStory userStory);

        //Read
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserStory GetUserStoryById(int userStoryId);

        //Update
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateUserStory(UserStory userStory);

        //Delete
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        void RemoveUserStoryById(int userStoryId);

        //Custom

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ICollection<UserStory> GetAllUserStories();

        [OperationContract]
        ICollection<UserStory> GetAllStoriesForProject(int projectId);
    }
}
