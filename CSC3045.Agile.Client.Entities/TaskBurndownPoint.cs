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
        private Dictionary<DateTime, int> _HoursRemaining;

        //Relationships
        private StoryTask _StoryTask;


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


        public StoryTask StoryTask
        {
            get { return _StoryTask; }
            set
            {
                if (_StoryTask != value)
                {
                    _StoryTask = value;
                    OnPropertyChanged(() => StoryTask);
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
                    OnPropertyChanged(() => HoursRemaining);
                }
            }
        }

    }
}
