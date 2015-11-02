using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using CSC3045.Agile.Client.Entities;


namespace CSC3045.Agile.Client.Entities
{
    public class PlanningPokerSession : ObjectBase
    {
        int _PlanningPokerSessionId;
        DateTime _StartTime;
        ISet<Account> _InvitedAccountSet;

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

        public ISet<Account> InvitedAccountSet
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
    }
}
