using Core.Common.Core;

namespace Core.Common.Tests
{
    internal class TestClass : ObjectBase
    {
        private string _CleanProp = string.Empty;
        private string _DirtyProp = string.Empty;
        private string _StringProp = string.Empty;

        public string CleanProp
        {
            get { return _CleanProp; }
            set
            {
                if (_CleanProp == value)
                    return;

                _CleanProp = value;
                OnPropertyChanged(() => CleanProp, false);
            }
        }

        public string DirtyProp
        {
            get { return _DirtyProp; }
            set
            {
                if (_DirtyProp == value)
                    return;

                _DirtyProp = value;
                OnPropertyChanged(() => DirtyProp);
            }
        }

        public string StringProp
        {
            get { return _StringProp; }
            set
            {
                if (_StringProp == value)
                    return;

                _StringProp = value;
                OnPropertyChanged("StringProp", false);
            }
        }

        public TestChild Child { get; set; }

        [NotNavigable]
        public TestChild NotNavigableChild { get; set; }  

        public CollectionBase<TestChild> Children { get; set; }
    }
}