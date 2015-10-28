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
        int _UserStoryId;
        string _StoryName;
        string _Title;
        string _Description;
        int _StoryPoints;
        string _UserNotes;
        StoryStatus _CurrentStatus;
        ISet<StoryTask> _AssociatedTasks;
        ISet<AcceptanceCriteria> _AcceptanceCriteria;

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

        public StoryStatus CurrentStatus
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
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

        public ISet<AcceptanceCriteria> Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => Status);
                }
            }
        }

        class UserRoleValidator : AbstractValidator<UserRole>
        {
            public UserRoleValidator()
            {
                RuleFor(obj => obj.UserRoleId).NotEmpty();
                RuleFor(obj => obj.UserRoleName).NotEmpty();
                RuleFor(obj => obj.PermissionLevel).GreaterThanOrEqualTo(0);
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new UserRoleValidator();
        }
    }
}
