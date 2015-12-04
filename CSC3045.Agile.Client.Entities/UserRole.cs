using System.Collections.Generic;
using System.Xml.Serialization;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class UserRole : ObjectBase
    {

        [XmlIgnore]
        private ICollection<Account> _Accounts;
        private int _UserRoleId;
        private string _UserRoleName;

        //Relationships
     
        public int UserRoleId
        {
            get { return _UserRoleId; }
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
            get { return _UserRoleName; }
            set
            {
                if (_UserRoleName != value)
                {
                    _UserRoleName = value;
                    OnPropertyChanged(() => UserRoleName);
                }
            }
        }
        [XmlIgnore]
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