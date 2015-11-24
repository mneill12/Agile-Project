using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts.Service_Contracts
{
    [ServiceContract]
    public interface IUserStoryService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserStory GetUserStoryInfo(int userStoryId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateUserStoryInfo(UserStory userStory);
    }
}
