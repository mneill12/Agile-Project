using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLStoryTask
    {
        private CurrentStatus _CurrentStatus;
        private string _Description;
        private int _Hours;
        private bool _IsBlocked;
        private XMLAccount _Owner;
        private int _StoryTaskId;
        private string _Title;
        private string _UserNotes;



        public int StoryTaskId
        {
            get { return _StoryTaskId; }
            set
            {
                if (_StoryTaskId != value)
                {
                    _StoryTaskId = value;
                   
                }
            }
        }

        public XMLAccount Owner
        {
            get { return _Owner; }
            set
            {
                if (_Owner != value)
                {
                    _Owner = value;
                    
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

        public int Hours
        {
            get { return _Hours; }
            set
            {
                if (_Hours != value)
                {
                    _Hours = value;
                   
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

        public bool IsBlocked
        {
            get { return _IsBlocked; }
            set
            {
                if (_IsBlocked != value)
                {
                    _IsBlocked = value;
                   
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
    }
}