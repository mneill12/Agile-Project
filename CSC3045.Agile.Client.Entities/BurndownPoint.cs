using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using CSC3045.Agile.Client.Entities;


namespace CSC3045.Agile.Client.Entities
{
    public class BurndownPoint : ObjectBase
    {
        int _BurndownPointId;
        int _ProjectId;
        int _SprintId;
        DateTime _BurndownPointDate;
        int _PointsRemaining;
        int _HoursRemaining;

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

        public int ProjectId
        {
            get { return _ProjectId; }
            set
            {
                if (_ProjectId != value)
                {
                    _ProjectId = value;
                    OnPropertyChanged(() => ProjectId);
                }
            }
        }

        public int SprintId
        {
            get { return _SprintId; }
            set
            {
                if (_SprintId != value)
                {
                    _SprintId = value;
                    OnPropertyChanged(() => SprintId);
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


    }
}
