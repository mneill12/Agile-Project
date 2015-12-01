using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class TaskBurndownPoint : ObjectBase
    {
        private int _TaskBurndownPointId;
        private int _StoryTaskId;
        private Dictionary<DateTime, int> _HoursRemaining;


        public int TaskBurndownPointID
        {
            get { return _TaskBurndownPointId; }
            set
            {
                if (_TaskBurndownPointId != value)
                {
                    _TaskBurndownPointId = value;
                    OnPropertyChanged(() => TaskBurndownPointID);
                }
            }
        }


        public int StoryTaskId
        {
            get { return _StoryTaskId; }
            set
            {
                if (_StoryTaskId != value)
                {
                    _StoryTaskId = value;
                    OnPropertyChanged(() => StoryTaskId);
                }
            }
        }

        public Dictionary<DateTime, int> HoursRemaining
        {
            get { return _HoursRemaining; }
            set
            {
                if (_HoursRemaining != value)
                {
                    _HoursRemaining = value;
                    OnPropertyChanged(() => StoryTaskId);
                }
            }
        }

    }
}
