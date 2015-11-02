using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Entities
{
    public class UserStory : ObjectBase
    {
        private int _UserStoryId;
        private string _StoryName;
        private string _Description;
        private int _StoryPoints;
        private string _UserNotes;
        private CurrentStatus _CurrentStatus;
        private ISet<StoryTask> _AssociatedTasks;
        private ISet<AcceptanceCriteria> _AcceptanceCriteria;

        public int UserStoryId
        {
            get
            {
                return _UserStoryId;
            }
            set
            {
                if (_UserStoryId != value)
                {
                    _UserStoryId = value;
                    OnPropertyChanged(() => UserStoryId);
                }
            }
        }

        public String StoryName
        {
            get
            {
                return _StoryName;
            }
            set
            {
                if (_StoryName != value)
                {
                    _StoryName = value;
                    OnPropertyChanged(() => StoryName);
                }
            }
        }

        public String Description
        {
            get
            {
                return _Description;
            }
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
            get
            {
                return _StoryPoints;
            }
            set
            {
                if (_StoryPoints != value)
                {
                    _StoryPoints = value;
                    OnPropertyChanged(() => StoryPoints);
                }
            }
        }

        public String UserNotes
        {
            get
            {
                return _UserNotes;
            }
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
            get
            {
                return _CurrentStatus;
            }
            set
            {
                if (_CurrentStatus != value)
                {
                    _CurrentStatus = value;
                    OnPropertyChanged(() => CurrentStatus);
                }
            }
        }

        public ISet<StoryTask> AssociatedTasks
        {
            get
            {
                return _AssociatedTasks;
            }
            set
            {
                if (_AssociatedTasks != value)
                {
                    _AssociatedTasks = value;
                    OnPropertyChanged(() => AssociatedTasks);
                }
            }
        }

        public ISet<AcceptanceCriteria> AcceptanceCriteria
        {
            get
            {
                return _AcceptanceCriteria;
            }
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
