using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace CSC3045.Agile.Client.Entities
{
    public class StoryTask : ObjectBase
    {
        private int _StoryTaskId;
        private Account _Owner;
        private string _Title;
        private string _Description;
        private int _Hours;
        private CurrentStatus _CurrentStatus;
        private bool _IsBlocked;
        private string _UserNotes;

        public int StoryTaskId
        {
            get
            {
                return _StoryTaskId;
            }
            set
            {
                if (_StoryTaskId != value)
                {
                    _StoryTaskId = value;
                    OnPropertyChanged(() => StoryTaskId);
                }
            }
        }

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

        public Account Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                if (_Owner != value)
                {
                    _Owner = value;
                    OnPropertyChanged(() => Owner);
                }
            }
        }

        public String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged(() => Title);
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

        public int Hours
        {
            get
            {
                return _Hours;
            }
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

        public Boolean IsBlocked
        {
            get
            {
                return _IsBlocked;
            }
            set
            {
                if (_IsBlocked != value)
                {
                    _IsBlocked = value;
                    OnPropertyChanged(() => IsBlocked);
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

    }
}
