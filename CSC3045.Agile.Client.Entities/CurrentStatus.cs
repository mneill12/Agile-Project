using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class CurrentStatus : ObjectBase
    {
        private int _StoryStatusId;
        private string _StoryStatusName;


        public int StoryStatusId
        {
            get { return _StoryStatusId; }
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
            get { return _StoryStatusName; }
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