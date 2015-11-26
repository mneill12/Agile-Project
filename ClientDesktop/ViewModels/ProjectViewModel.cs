using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Entities;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectViewModel : ViewModelBase
    {
        private IRegionManager _RegionManager;
        private IServiceFactory _ServiceFactory;

        [ImportingConstructor]
        public ProjectViewModel(IServiceFactory serviceFactory, IRegionManager regionManager, Project project)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;
        }

        protected override void OnViewLoaded()
        {
        }
    }
}