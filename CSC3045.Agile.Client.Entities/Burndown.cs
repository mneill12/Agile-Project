using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Entities
{
    public class Burndown : ObjectBase
    {
        private int _BurndownId;
        private string _BurndownName;
        private ICollection<BurndownPoint> _BurndownPoints;

        public int BurndownId
        {
            get { return _BurndownId; }
            set
            {
                if (_BurndownId != value)
                {
                    _BurndownId = value;
                    OnPropertyChanged(() => BurndownId);
                }
            }
        }

        public string BurndownName
        {
            get { return _BurndownName; }
            set
            {
                if (_BurndownName != value)
                {
                    _BurndownName = value;
                    OnPropertyChanged(() => BurndownName);
                }
            }
        }

        public ICollection<BurndownPoint> BurndownPoints
        {
            get { return _BurndownPoints; }
            set
            {
                if (_BurndownPoints != value)
                {
                    _BurndownPoints = value;
                    OnPropertyChanged(() => BurndownPoints);
                }
            }
        }

    }
}
