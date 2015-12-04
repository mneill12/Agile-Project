using Core.Common.Core;

namespace CSC3045.Agile.Client.Entities
{
    public class Criteria : ObjectBase
    {
        private int _CriteriaId;
        private string _CriteriaOutline;
        private string _CriteriaType;

        public int CriteriaId
        {
            get { return _CriteriaId; }
            set
            {
                if (_CriteriaId != value)
                {
                    _CriteriaId = value;
                    OnPropertyChanged(() => CriteriaId);
                }
            }
        }

        public string CriteriaType
        {
            get { return _CriteriaType; }
            set
            {
                if (_CriteriaType != value)
                {
                    _CriteriaType = value;
                    OnPropertyChanged(() => CriteriaType);
                }
            }
        }

        public string CriteriaOutline
        {
            get { return _CriteriaOutline; }
            set
            {
                if (_CriteriaOutline != value)
                {
                    _CriteriaOutline = value;
                    OnPropertyChanged(() => CriteriaOutline);
                }
            }
        }
    }
}