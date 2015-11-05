using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Entities
{
    public class Account : ObjectBase
    {
        private int _AccountId;
        private string _LoginEmail;
        private string _Password;
        private string _FirstName;
        private string _LastName;
        private ISet<UserRole> _UserRoles;
        private ISet<Project> _AssocicatedProjects;
        private ISet<Sprint> _AssociatedSprints;
        private ISet<PlanningPokerSession> _AssociatedPlanningPokerSessions;

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

        public ISet<UserRole> UserRoles
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

        public ISet<Project> AssocicatedProjects
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

        public ISet<Sprint> AssociatedSprints
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

        public ISet<PlanningPokerSession> AssociatedPlanningPokerSessions
        {
            get { return _AssociatedPlanningPokerSessions; }
            set
            {
                if (_AssociatedPlanningPokerSessions != value)
                {
                    _AssociatedPlanningPokerSessions = value;
                    OnPropertyChanged(() => AssociatedPlanningPokerSessions);
                }
            }
        }

    }
}
