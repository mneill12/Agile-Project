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

        public UserStory GetUserStoryById(int userStoryId)
        {
            return Channel.GetUserStoryById(userStoryId);
        }

        public void UpdateUserStory(UserStory userStory)
        {
            Channel.UpdateUserStory(userStory);
        }

        public void RemoveUserStory(int userStoryId)
        {
            Channel.RemoveUserStory(userStoryId);
        }

        public ICollection<UserStory> GetAllUserStories()
        {
            return Channel.GetAllUserStories();
        }

        public ICollection<UserStory> GetAllStoriesForProject(int projectId)
        {
            return Channel.GetAllStoriesForProject(projectId);
        }
    }
}
