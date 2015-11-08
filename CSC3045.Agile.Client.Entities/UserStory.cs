﻿using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class UserStory : ObjectBase
    {
        private ICollection<AcceptanceCriteria> _AcceptanceCriteria;
        private ICollection<StoryTask> _AssociatedTasks;
        private CurrentStatus _CurrentStatus;
        private string _Description;
        private string _StoryName;
        private int _StoryPoints;
        private string _UserNotes;
        private int _UserStoryId;

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

        public string StoryName
        {
            get { return _StoryName; }
            set
            {
                if (_StoryName != value)
                {
                    _StoryName = value;
                    OnPropertyChanged(() => StoryName);
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