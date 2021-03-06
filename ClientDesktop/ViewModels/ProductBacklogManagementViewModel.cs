﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ClientDesktop.Views;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
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
        private string _StoryNumberText;
        private string _StoryDescText;
        private string _StoryPointsText;

        private int currentProjectId;

        [ImportingConstructor]
        public ProductBacklogManagementViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            _BacklogStories = new List<UserStory>();

            AddNewStoryCommand = new DelegateCommand<object>(OnAddNewStory);
            UpdateStoryCommand = new DelegateCommand<object>(OnUpdateStory);
            RemoveStoryCommand = new DelegateCommand<object>(OnRemoveStory);
            NavigateDashboardCommand = new DelegateCommand<object>(NavigateDashboard);
        }

        public DelegateCommand<object> AddNewStoryCommand { get; private set; }
        public DelegateCommand<object> UpdateStoryCommand { get; private set; }
        public DelegateCommand<object> RemoveStoryCommand { get; private set; }
        public DelegateCommand<object> NavigateDashboardCommand { get; set; }

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

        public string StoryNumberText
        {
            get { return _StoryNumberText; }
            set
            {
                if (_StoryNumberText == value) return;
                _StoryNumberText = value;
                OnPropertyChanged("StoryNumberText");
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

        public string StoryPointsText
        {
            get { return _StoryPointsText; }
            set
            {
                if (_StoryPointsText == value) return;
                _StoryPointsText = value;
                OnPropertyChanged("StoryPointsText");
            }
        }

        public UserStory SelectedUserStory
        {
            get { return _SelectedUserStory; }
            set
            {
                if (_SelectedUserStory == value) return;
                _SelectedUserStory = value;
                if (SelectedUserStory != null)
                {
                    StoryNumberText = value.StoryNumber;
                    StoryDescText = value.Description;
                    StoryPointsText = value.StoryPoints.ToString();
                }

                OnPropertyChanged("SelectedUserStory");
            }
        }

        private void NavigateDashboard(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(DashboardView).FullName);
        }

        public override  void OnNavigatedTo(NavigationContext navigationContext)
        {
            var id = (int)navigationContext.Parameters["projectId"];
            currentProjectId = id;
            OnViewLoaded();
        }

        protected override void OnViewLoaded()
        {
            GetUserStories();
        }

        protected void GetUserStories()
        {
            WithClient(_ServiceFactory.CreateClient<IUserStoryService>(), userStoryClient =>
            {
                ICollection<UserStory> stories = userStoryClient.GetAllStoriesForProject(currentProjectId);
                BacklogStories = stories.ToList();
            });
        }

        protected void OnRemoveStory(object parameter)
        {
            if (SelectedUserStory != null)
            {
                int storyIndex = _BacklogStories.IndexOf(SelectedUserStory);
                UserStory story = _BacklogStories.ElementAt(storyIndex);

                WithClient(_ServiceFactory.CreateClient<IUserStoryService>(), userStoryClient =>
                {
                    userStoryClient.RemoveUserStoryById(story.UserStoryId);
                });
            }
            GetUserStories();
        }

        protected void OnUpdateStory(object parameter)
        {
            if (SelectedUserStory != null)
            {
                int storyIndex = _BacklogStories.IndexOf(SelectedUserStory);
                UserStory story = _BacklogStories.ElementAt(storyIndex);
                story.StoryNumber = StoryNumberText;
                story.Description = StoryDescText;
                story.StoryPoints = Int32.Parse(StoryPointsText);

                WithClient(_ServiceFactory.CreateClient<IUserStoryService>(), userStoryClient =>
                {
                    userStoryClient.UpdateUserStory(story);
                });

                StoryNumberText = "";
                StoryDescText = "";

                GetUserStories();
            }
        }

        protected void OnAddNewStory(object parameter)
        {
            Project project = null;

            WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
            {
                project = projectClient.GetProjectInfo(currentProjectId);
            });

            var newUserStory = new UserStory
            {
                StoryNumber = "New Story Name",
                Description = "New Story Description",
                StoryPoints = 0,
                Project = project
            };

            newUserStory.CleanAll();

            WithClient(_ServiceFactory.CreateClient<IProjectService>(), projectClient =>
            {
                projectClient.AddUserStoryToProject(project.ProjectId, newUserStory);
            });

            GetUserStories();
        }

        
    }
}
