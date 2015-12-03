using System;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class BurndownPoint : ObjectBase
    {
        private DateTime _BurndownPointDate;
        private int _BurndownPointId;
        private int _HoursRemaining;
        private int _PointsRemaining;

        //Relationships
        private Burndown _Burndown;

        public int BurndownPointId
        {
            get { return _BurndownPointId; }
            set
            {
                if (_BurndownPointId != value)
                {
                    _BurndownPointId = value;
                    OnPropertyChanged(() => BurndownPointId);
                }
            }
        }

        public DateTime BurndownPointDate
        {
            get { return _BurndownPointDate; }
            set
            {
                if (_BurndownPointDate != value)
                {
                    _BurndownPointDate = value;
                    OnPropertyChanged(() => BurndownPointDate);
                }
            }
        }

        public int PointsRemaining
        {
            get { return _PointsRemaining; }
            set
            {
                if (_PointsRemaining != value)
                {
                    _PointsRemaining = value;
                    OnPropertyChanged(() => PointsRemaining);
                }
            }
        }

        public int HoursRemaining
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

        public Burndown Burndown
        {
            get { return _Burndown; }
            set
            {
                if (_Burndown != value)
                {
                    _Burndown = value;
                    OnPropertyChanged(() => Burndown);
                }
            }
        }
    }
}