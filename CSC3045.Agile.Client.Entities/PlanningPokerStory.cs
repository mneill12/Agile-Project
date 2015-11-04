using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using CSC3045.Agile.Client.Entities;


namespace CSC3045.Agile.Client.Entities
{
    public class PlanningPokerStory : ObjectBase
    {
        int _PlanningPokerStoryId;
        int _PlanningPokerSessionId;
        int _UserStoryId;

        public int PlanningPokerStoryId
        {
            get { return _PlanningPokerStoryId; }
            set
            {
                if (_PlanningPokerStoryId != value)
                {
                    _PlanningPokerStoryId = value;
                    OnPropertyChanged(() => PlanningPokerStoryId);
                }
            }
        }

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

        public int UserStoryId
        {
            get { return _UserStoryId; }
            set
            {
                if (_UserStoryId != value)
                {
                    _UserStoryId = value;
                    OnPropertyChanged(() => UserStoryId);
                }
            }
        }

    }
}
