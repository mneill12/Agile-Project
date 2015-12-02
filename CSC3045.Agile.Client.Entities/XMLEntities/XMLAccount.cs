using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLAccount : ObjectBase
    {
        private int _AccountId;
        
        private List<XMLSprint> _AssociatedSprints;
        private List<XMLProject> _AssocicatedProjects;
        private string _FirstName;
        private string _LastName;
        private string _LoginEmail;
        private string _Password;
        private List<UserRole> _UserRoles;
        private List<Skill> _Skills;

        public int AccountId
        {
            get { return _AccountId; }
            set
            {
                if (_AccountId != value)
                {
                    _AccountId = value;
                    OnPropertyChanged(() => AccountId);
                }
            }
        }

        public string LoginEmail
        {
            get { return _LoginEmail; }
            set
            {
                if (_LoginEmail != value)
                {
                    _LoginEmail = value;
                    OnPropertyChanged(() => LoginEmail);
                }
            }
        }

        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged(() => Password);
                }
            }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }

        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                    OnPropertyChanged(() => LastName);
                }
            }
        }

        public List<UserRole> UserRoles
        {
            get { return _UserRoles; }
            set
            {
                if (_UserRoles != value)
                {
                    _UserRoles = value;
                    OnPropertyChanged(() => UserRoles);
                }
            }
        }

        public List<Skill> Skills
        {
            get { return _Skills; }
            set
            {
                if (_Skills != value)
                {
                    _Skills = value;
                    OnPropertyChanged(() => Skills);
                }
            }
        }

        public List<XMLProject> AssocicatedProjects
        {
            get { return _AssocicatedProjects; }
            set
            {
                if (_AssocicatedProjects != value)
                {
                    _AssocicatedProjects = value;
                    OnPropertyChanged(() => AssocicatedProjects);
                }
            }
        }

        public List<XMLSprint> AssociatedSprints
        {
            get { return _AssociatedSprints; }
            set
            {
                if (_AssociatedSprints != value)
                {
                    _AssociatedSprints = value;
                    OnPropertyChanged(() => AssociatedSprints);
                }
            }
        }
    }
}