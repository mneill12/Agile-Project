using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlanningPokerSessionParticipantViewModel : ViewModelBase
    {
        private IRegionManager _RegionManager;
        private IServiceFactory _ServiceFactory;

        [ImportingConstructor]
        public PlanningPokerSessionParticipantViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;
        }

        protected override void OnViewLoaded()
        {
        }
    }
}