using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.UI.Core;
using Prism.Regions;

namespace ClientDesktop
{
    class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<object> NavigateCommand { get; private set; } 

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<object>(Navigate);
        }

        private void Navigate(object navigatePath)
        {
            if(navigatePath != null)
                _regionManager.RequestNavigate("MainRegion", navigatePath.ToString());
        }
    }
}
