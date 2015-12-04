using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class CurrentStatus : ObjectBase
    {
        private int _CurrentStatusId;
        private string _CurrentStatusName;

        private ICollection<UserStory> _AssociatedUserStories;
        private ICollection<StoryTask> _AssociatedStoryTasks;


        public int CurrentStatusId
        {
            get { return _CurrentStatusId; }
            set
            {
                if (_CurrentStatusId != value)
                {
                    _CurrentStatusId = value;
                    OnPropertyChanged(() => CurrentStatusId);
                }
            }
        }

        public string CurrentStatusName
        {
            get { return _CurrentStatusName; }
            set
            {
                if (_CurrentStatusName != value)
                {
                    _CurrentStatusName = value;
                    OnPropertyChanged(() => CurrentStatusName);
                }
            }
        }
    }
}