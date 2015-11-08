using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class Backlog : ObjectBase
    {
        private ICollection<int> _AssociatedUserStoryIdSet;
        private int _BacklogId;

        public int BacklogId
        {
            get { return _BacklogId; }
            set
            {
                if (_BacklogId != value)
                {
                    _BacklogId = value;
                    OnPropertyChanged(() => BacklogId);
                }
            }
        }

        public ICollection<int> AssociatedUserStoryIdSet
        {
            get { return _AssociatedUserStoryIdSet; }
            set
            {
                if (_AssociatedUserStoryIdSet != value)
                {
                    _AssociatedUserStoryIdSet = value;
                    OnPropertyChanged(() => AssociatedUserStoryIdSet);
                }
            }
        }
    }
}