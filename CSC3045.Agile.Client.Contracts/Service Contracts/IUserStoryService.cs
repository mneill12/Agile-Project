using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IUserStoryService : IServiceContract
    {
        [OperationContract]
        UserStory AddNewUserStory(UserStory userStory);
    }
}
