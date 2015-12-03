using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class Skill : ObjectBase
    {
        private int _SkillId;
        private string _SkillName;

        //Relationships
        private ICollection<Account> _Accounts;

        public int SkillId
        {
            get { return _SkillId; }
            set
            {
                if (_SkillId != value)
                {
                    _SkillId = value;
                    OnPropertyChanged(() => SkillId);
                }
            }
        }

        public string SkillName
        {
            get { return _SkillName; }
            set
            {
                if (_SkillName != value)
                {
                    _SkillName = value.ToLower();
                    OnPropertyChanged(() => SkillName);
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