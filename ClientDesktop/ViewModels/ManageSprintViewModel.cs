using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ClientDesktop.Views;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using Core.Common.Utils;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.CustomPrinciples;
using CSC3045.Agile.Client.Entities;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManageSprintViewModel : ViewModelBase
    {


        #region Private vars
        private readonly IServiceFactory _ServiceFactory;
        private readonly IRegionManager _RegionManager;

        private int _CurrentProjectId;
        private int _CurrentSprintId;
        private List<UserStory> _BacklogUserStories;
        private List<UserStory> _SprintUserStories;
        #endregion
                                          
        public DelegateCommand<object> NavigateDashboardCommand { get; private set; }
        public int currentProjectId
        {
            get { return _CurrentProjectId; }
            set
            {
                if (_CurrentProjectId == value) return;
                _CurrentProjectId = value;
                OnPropertyChanged("ProjectId");
            }
        }

        public int currentSprintId
        {
            get { return _CurrentSprintId; }
            set
            {
                if (_CurrentSprintId == value) return;
                _CurrentSprintId = value;
                OnPropertyChanged("SprintId");
            }
        }

        public List<UserStory> BacklogUserStories
        {
            get { return _BacklogUserStories; }
            set
            {
                if (_BacklogUserStories == value) return;
                _BacklogUserStories= value;
                OnPropertyChanged("BacklogUserStories");
            }
        }

        public List<UserStory> SprintUserStories
        {
            get { return _SprintUserStories; }
            set
            {
                if (_SprintUserStories == value) return;
                _SprintUserStories = value;
                OnPropertyChanged("SprintUserStories");
            }
        }

        [ImportingConstructor]
        public ManageSprintViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;
            NavigateDashboardCommand = new DelegateCommand<object>(NavigateDashboard);
        }

        protected override void OnViewLoaded()
        {
           GetUserStoriesForProject();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {

            var projectId = (int)navigationContext.Parameters["projectId"];
           // var sprintId = (int)navigationContext.Parameters["sprintId"];
            currentProjectId = projectId;
   
            OnViewLoaded();
        }

        private void NavigateDashboard(object parameter)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(DashboardView).FullName);
        }
      
        public void GetUserStoriesForProject()
        {
            WithClient(_ServiceFactory.CreateClient<IUserStoryService>(), userStoryClient =>
            {
                ICollection<UserStory> stories = userStoryClient.GetAllStoriesForProject(currentProjectId);
                BacklogUserStories= stories.ToList();
            });
        }
        
    }
}