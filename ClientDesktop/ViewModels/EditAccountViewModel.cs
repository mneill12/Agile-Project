using System.ComponentModel.Composition;
using System.Windows.Controls;
using ClientDesktop.Views;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EditAccountViewModel : ViewModelBase
    {
        IServiceFactory _ServiceFactory;
        IRegionManager _RegionManager;

        [ImportingConstructor]
        public EditAccountViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;
        }

        protected override void OnViewLoaded()
        {

        }
    }
}
