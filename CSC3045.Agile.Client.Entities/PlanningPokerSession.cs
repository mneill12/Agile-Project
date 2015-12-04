using System;
using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class PlanningPokerSession : ObjectBase
    {
        private ICollection<Account> _InvitedAccountSet;
        private int _PlanningPokerSessionId;
        private DateTime _StartTime;
        private ICollection<UserStory> _UserStories;
        private ICollection<UserStory> _CompletedUserStories;
        private ICollection<ChatMessage> _Messages;
        private Account _ScrumMaster;

        public int PlanningPokerSessionId
        {
            get { return _PlanningPokerSessionId; }
            set
            {
                if (_PlanningPokerSessionId != value)
                {
                    _PlanningPokerSessionId = value;
                    OnPropertyChanged(() => PlanningPokerSessionId);
                }
            }
        }

        public DateTime StartTime
        {
            get { return _StartTime; }
            set
            {
                if (_StartTime != value)
                {
                    _StartTime = value;
                    OnPropertyChanged(() => StartTime);
                }
            }
        }

        public ICollection<Account> InvitedAccountSet
        {
            get { return _InvitedAccountSet; }
            set
            {
                if (_InvitedAccountSet != value)
                {
                    _InvitedAccountSet = value;
                    OnPropertyChanged(() => InvitedAccountSet);
                }
            }
        }

        public ICollection<UserStory> UserStories
        {
            get { return _UserStories; }
            set
            {
                if (_UserStories != value)
                {
                    _UserStories = value;
                    OnPropertyChanged(() => UserStories);
                }
            }
        }

        public Account ScrumMaster
        {
            get { return _ScrumMaster; }
            set
            {
                if (_ScrumMaster != value)
                {
                    _ScrumMaster = value;
                    OnPropertyChanged(() => ScrumMaster);
                }
            }
        }

        public ICollection<UserStory> CompletedUserStories
        {
            get { return _CompletedUserStories; }
            set
            {
                if (_CompletedUserStories != value)
                {
                    _CompletedUserStories = value;
                    OnPropertyChanged(() => CompletedUserStories);
                }
            }
        }

        public ICollection<ChatMessage> Messages
        {
            get { return _Messages; }
            set
            {
                if (_Messages != value)
                {
                    _Messages = value;
                    OnPropertyChanged(() => Messages);
                }
            }
        }
    }
}