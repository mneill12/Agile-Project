using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace CSC3045.Agile.Client.Entities
{
    public class StoryStatus : ObjectBase
    {
        int _UserRoleId;
        string _UserRoleName;
        int _PermissionLevel;

        public int UserRoleId
        {
            get
            {
                return _UserRoleId;
            }
            set
            {
                if (_UserRoleId != value)
                {
                    _UserRoleId = value;
                    OnPropertyChanged(() => UserRoleId);
                }
            }
        }

        public String UserRoleName
        {
            get
            {
                return _UserRoleName;
            }
            set
            {
                if (_UserRoleName != value)
                {
                    _UserRoleName = value;
                    OnPropertyChanged(() => UserRoleName);
                }
            }
        }

        public int PermissionLevel
        {
            get
            {
                return _PermissionLevel;
            }
            set
            {
                if (_PermissionLevel != value)
                {
                    _PermissionLevel = value;
                    OnPropertyChanged(() => PermissionLevel);
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
