using System.Collections.Generic;
using System.Xml.Serialization;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class Burndown : ObjectBase
    {
        private int _BurndownId;
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

        [XmlIgnore]
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