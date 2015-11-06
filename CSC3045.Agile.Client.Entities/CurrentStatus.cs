using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace CSC3045.Agile.Client.Entities
{
    public class CurrentStatus : ObjectBase
    {
        int _StoryStatusId;
        string _StoryStatusName;
      

        public int StoryStatusId
        {
            get
            {
                return _StoryStatusId;
            }
            set
            {
                if (_StoryStatusId != value)
                {
                    _StoryStatusId = value;
                    OnPropertyChanged(() => StoryStatusId);
                }
            }
        }

        public string StoryStatusName
        {
            get
            {
                return _StoryStatusName;
            }
            set
            {
                if (_StoryStatusName != value)
                {
                    _StoryStatusName = value;
                    OnPropertyChanged(() => StoryStatusName);
                }
            }
        }
    }
}
