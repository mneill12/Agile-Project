using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class StoryTask : ObjectBase
    {
        private string _Description;
        private int _Hours;
        private bool _IsBlocked;
        private int _StoryTaskId;
        private string _Title;
        private string _UserNotes;

        //Relationships
        private Account _Owner;
        private CurrentStatus _CurrentStatus;
        private UserStory _UserStory;
        private TaskBurndownPoint _TaskBurndownPoint;



        public int StoryTaskId
        {
            get { return _StoryTaskId; }
            set
            {
                if (_StoryTaskId != value)
                {
                    _StoryTaskId = value;
                    OnPropertyChanged(() => StoryTaskId);
                }
            }
        }

        public Account Owner
        {
            get { return _Owner; }
            set
            {
                if (_Owner != value)
                {
                    _Owner = value;
                    OnPropertyChanged(() => Owner);
                }
            }
        }

        public TaskBurndownPoint TaskBurnDownPoint
        {
            get { return _TaskBurndownPoint; }
            set
            {
                if (_TaskBurndownPoint != value)
                {
                    _TaskBurndownPoint = value;
                    OnPropertyChanged(() =>  TaskBurnDownPoint);
                }
            }
        }

        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged(() => Title);
                }
            }
        }


        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public int Hours
        {
            get { return _Hours; }
            set
            {
                if (_Hours != value)
                {
                    _Hours = value;
                    OnPropertyChanged(() => Hours);
                }
            }
        }

        public CurrentStatus CurrentStatus
        {
            get { return _CurrentStatus; }
            set
            {
                if (_CurrentStatus != value)
                {
                    _CurrentStatus = value;
                    OnPropertyChanged(() => CurrentStatus);
                }
            }
        }

        public bool IsBlocked
        {
            get { return _IsBlocked; }
            set
            {
                if (_IsBlocked != value)
                {
                    _IsBlocked = value;
                    OnPropertyChanged(() => IsBlocked);
                }
            }
        }

        public string UserNotes
        {
            get { return _UserNotes; }
            set
            {
                if (_UserNotes != value)
                {
                    _UserNotes = value;
                    OnPropertyChanged(() => UserNotes);
                }
            }
        }

        public UserStory UserStory
        {
            get { return _UserStory; }
            set
            {
                if (_UserStory != value)
                {
                    _UserStory = value;
                    OnPropertyChanged(() => UserStory);
                }
            }
        }
    }
}