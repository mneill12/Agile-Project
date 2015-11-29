using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts.Service_Contracts
{
    [ServiceContract]
    public interface IUserStoryService
    {
        [OperationContract]
        UserStory AddNewUserStory(UserStory userStory);
    }
}
