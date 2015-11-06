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

        public DelegateCommand<string> NavigateCommand { get; set; } 

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if(navigatePath != null)
                _regionManager.RequestNavigate("MainRegion", navigatePath);
        }
    }
}
