using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class Criteria : ObjectBase
    {
        private int _criteriaId;
        private string _criteriaOutline;
        private string _criteriaType;

        public int CriteriaId
        {
            get { return _criteriaId; }
            set
            {
                if (_criteriaId != value)
                {
                    _criteriaId = value;
                    OnPropertyChanged(() => CriteriaId);
                }
            }
        }

        public string CriteriaType
        {
            get { return _criteriaType; }
            set
            {
                if (_criteriaType != value)
                {
                    _criteriaType = value;
                    OnPropertyChanged(() => CriteriaType);
                }
            }
        }

        public string CriteriaOutline
        {
            get { return _criteriaOutline; }
            set
            {
                if (_criteriaOutline != value)
                {
                    _criteriaOutline = value;
                    OnPropertyChanged(() => CriteriaOutline);
                }
            }
        }
    }
}