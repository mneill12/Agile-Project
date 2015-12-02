using System.Collections.Generic;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities.XMLEntities
{
    public class XMLBacklog
    {
        private List<int> _AssociatedUserStoryIdSet;
        private int _BacklogId;

        public int BacklogId
        {
            get { return _BacklogId; }
            set
            {
                if (_BacklogId != value)
                {
                    _BacklogId = value;
                  
                }
            }
        }

        public List<int> AssociatedUserStoryIdSet
        {
            get { return _AssociatedUserStoryIdSet; }
            set
            {
                if (_AssociatedUserStoryIdSet != value)
                {
                    _AssociatedUserStoryIdSet = value;
                   
                }
            }
        }
    }
}