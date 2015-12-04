using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class UserStory : ObjectBase
    {
        private int _UserStoryId { get; set; }
        private string _StoryNumber { get; set; }
        private string _Description { get; set; }
        private int _StoryPoints { get; set; }
        private string _UserNotes { get; set; }

        //Relationships
        private CurrentStatus _Status { get; set; }
        private ICollection<StoryTask> _AssociatedTasks { get; set; }
        private ICollection<AcceptanceCriteria> _AcceptanceCriteria { get; set; }
        private Project _Project { get; set; }
        private Sprint _Sprint { get; set; }

        public Project Project
        {
            get { return _Project; }
            set
            {
                if (_Project != value)
                {
                    _Project = value;
                    OnPropertyChanged(() => Project);
                }
            }
        }

        public Sprint Sprint
        {
            get { return _Sprint; }
            set
            {
                if (_Sprint != value)
                {
                    _Sprint = value;
                    OnPropertyChanged(() => Sprint);
                }
            }
        }

        public int UserStoryId
        {
            get { return _UserStoryId; }
            set
            {
                if (_UserStoryId != value)
                {
                    _UserStoryId = value;
                    OnPropertyChanged(() => UserStoryId);
                }
            }
        }

        public string StoryNumber
        {
            get { return _StoryNumber; }
            set
            {
                if (_StoryNumber != value)
                {
                    _StoryNumber = value;
                    OnPropertyChanged(() => StoryNumber);
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

        public int StoryPoints
        {
            get { return _StoryPoints; }
            set
            {
                if (_StoryPoints != value)
                {
                    _StoryPoints = value;
                    OnPropertyChanged(() => StoryPoints);
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

        public CurrentStatus CurrentStatus
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => CurrentStatus);
                }
            }
        }

        public ICollection<StoryTask> AssociatedTasks
        {
            get { return _AssociatedTasks; }
            set
            {
                if (_AssociatedTasks != value)
                {
                    _AssociatedTasks = value;
                    OnPropertyChanged(() => AssociatedTasks);
                }
            }
        }

        public ICollection<AcceptanceCriteria> AcceptanceCriteria
        {
            get { return _AcceptanceCriteria; }
            set
            {
                if (_AcceptanceCriteria != value)
                {
                    _AcceptanceCriteria = value;
                    OnPropertyChanged(() => AcceptanceCriteria);
                }
            }
        }
    }
}