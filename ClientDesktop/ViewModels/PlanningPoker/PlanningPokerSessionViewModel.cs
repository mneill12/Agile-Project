using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlanningPokerSessionViewModel : ViewModelBase
    {
        private IRegionManager _RegionManager;
        private IServiceFactory _ServiceFactory;

        [ImportingConstructor]
        public PlanningPokerSessionViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;
        }

        protected override void OnViewLoaded()
        {
        }
    }
}