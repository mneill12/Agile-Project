using System.Collections.Generic;
using Core.Common.Core;
using CSC3045.Agile.Client.Entities.XMLEntities;

namespace CSC3045.Agile.Client.Entities
{
    public class XMLUserStory
    {
        private List<XMLAcceptanceCriteria> _AcceptanceCriteria;
        private List<XMLStoryTask> _AssociatedTasks;
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
                    
                }
            }
        }

        public List<XMLStoryTask> AssociatedTasks
        {
            get { return _AssociatedTasks; }
            set
            {
                if (_AssociatedTasks != value)
                {
                    _AssociatedTasks = value;
                   
                }
            }
        }

        public List<XMLAcceptanceCriteria> AcceptanceCriteria
        {
            get { return _AcceptanceCriteria; }
            set
            {
                if (_AcceptanceCriteria != value)
                {
                    _AcceptanceCriteria = value;
                   
                }
            }
        }
    }
}