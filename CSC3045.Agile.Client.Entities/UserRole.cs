using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace CSC3045.Agile.Client.Entities
{
    public class UserRole : ObjectBase
    {
        private int _UserRoleId;
        private string _UserRoleName;
        private ICollection<Account> _Accounts;

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

        public string UserRoleName
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

        public ICollection<Account> Accounts
        {
            get { return _Accounts; }
            set
            {
                if (_Accounts != value)
                {
                    _Accounts = value;
                    OnPropertyChanged(() => Accounts);
                }
            }
        }
    }
}
