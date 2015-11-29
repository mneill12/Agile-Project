using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows.Data;
using ClientDesktop.Views;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductBacklogManagementViewModel : ViewModelBase
    {
        private readonly IServiceFactory _ServiceFactory;
        private readonly IRegionManager _RegionManager;

        private List<UserStory> _BacklogStories;
        private UserStory _SelectedUserStory;
        private string _StoryNameText;
        private string _StoryDescText;

        [ImportingConstructor]
        public ProductBacklogManagementViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            _BacklogStories = new List<UserStory>();
            _BacklogStories.Add(new UserStory() { StoryName = "CSC-001", Description = "desc for user story 1" });
            _BacklogStories.Add(new UserStory() { StoryName = "CSC-002", Description = "desc for user story 2" });
            _BacklogStories.Add(new UserStory() { StoryName = "CSC-003", Description = "desc for user story 3" });

            AddNewStoryCommand = new DelegateCommand<object>(OnAddNewStory);
            UpdateStoryCommand = new DelegateCommand<object>(OnUpdateStory);
            RemoveStoryCommand = new DelegateCommand<object>(OnRemoveStory);
        }

        public DelegateCommand<object> AddNewStoryCommand { get; private set; }
        public DelegateCommand<object> UpdateStoryCommand { get; private set; }
        public DelegateCommand<object> RemoveStoryCommand { get; private set; } 

        public List<UserStory> BacklogStories
        {
            get { return _BacklogStories; }
            set
            {
                if (_BacklogStories == value) return;
                _BacklogStories = value;
                OnPropertyChanged("BacklogStories");
            }
        }

        public string StoryNameText
        {
            get { return _StoryNameText; }
            set
            {
                if (_StoryNameText == value) return;
                _StoryNameText = value;
                OnPropertyChanged("StoryNameText");
            }
        }

        public string StoryDescText
        {
            get { return _StoryDescText; }
            set
            {
                if (_StoryDescText == value) return;
                _StoryDescText = value;
                OnPropertyChanged("StoryDescText");
            }
        }

        public UserStory SelectedUserStory
        {
            get { return _SelectedUserStory; }
            set
            {
                if (_SelectedUserStory == value) return;
                _SelectedUserStory = value;
                StoryNameText = value.StoryName;
                StoryDescText = value.Description;
                OnPropertyChanged("SelectedUserStory");
            }
        }

        protected override void OnViewLoaded()
        {
            GetUserStories();
        }

        protected void GetUserStories()
        {
            /*
             * Database logic still needs to be implemented 

            /*ICollection<int> userStoryIds;

            WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
            {
                Project project =  projectClient.GetProjectInfo(ServiceLocator.Current.GetInstance<DashboardViewModel>().CurrentProjectId);
                
                userStoryIds = project.Backlog.AssociatedUserStoryIdSet;
            });

            WithClient(_ServiceFactory.CreateClient<IUserStoryService>(), userStoryClient =>
            {
                int currentProjectId = ServiceLocator.Current.GetInstance<DashboardViewModel>().CurrentProjectId;
                var userStories = userStoryClient.

                if (null != userStories && userStories.Any())
                {
                    BacklogStories.AddRange(userStories);
                }
            });*/

            
        }

        protected void OnRemoveStory(object parameter)
        {
            if (SelectedUserStory != null)
            {
                BacklogStories.Remove(SelectedUserStory);
            }
        }

        protected void OnUpdateStory(object parameter)
        {
            if (SelectedUserStory != null)
            {
                int storyIndex = _BacklogStories.IndexOf(SelectedUserStory);
                UserStory story = _BacklogStories.ElementAt(storyIndex);
                story.StoryName = StoryNameText;
                story.Description = StoryDescText;

                StoryNameText = "";
                StoryDescText = "";
            }
        }

        protected void OnAddNewStory(object parameter)
        {

            List<UserStory> newBacklog = _BacklogStories;
            newBacklog.Add(new UserStory{Description = "Add Story Description", StoryName = "Add Story Name"});
            BacklogStories = newBacklog;

            /*
             * Database logic still needs to be implemented

            /*var newUserStory = new UserStory
            {
                StoryName = "New Story Name",
                Description = "New Story Description"
            };

            UserStory createdUserStory = null;
            Project currentProject = null;

            WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
            {
                currentProject =
                    projectClient.GetProjectInfo(
                        ServiceLocator.Current.GetInstance<DashboardViewModel>().CurrentProjectId);
            });


            WithClient(_ServiceFactory.CreateClient<IUserStoryService>(), userStoryClient =>
            {
                createdUserStory = userStoryClient.AddNewUserStory(newUserStory);
            });

            currentProject.Backlog.AssociatedUserStoryIdSet.Add(createdUserStory.UserStoryId);*/
        }
    }
}
