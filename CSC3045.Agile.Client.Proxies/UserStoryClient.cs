using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof(IUserStoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class UserStoryClient : ClientBase<IUserStoryService>, IUserStoryService
    {
        public UserStory AddNewUserStory(UserStory userStory)
        {
            return Channel.AddNewUserStory(userStory);
        }
    }
}
