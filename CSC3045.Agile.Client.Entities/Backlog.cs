﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Entities
{
    public class Backlog : ObjectBase
    {
        private int _BacklogId;
        private ISet<int> _AssociatedUserStoryIdSet;

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

        public ISet<int> AssociatedUserStoryIdSet
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