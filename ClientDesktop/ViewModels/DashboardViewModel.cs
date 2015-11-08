using System.ComponentModel.Composition;
using ClientDesktop.Views;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IRegionManager _RegionManager;

        private IServiceFactory _ServiceFactory;

        [ImportingConstructor]
        public DashboardViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            AddProjectCommand = new DelegateCommand<object>(OnAddProject);
        }

        public DelegateCommand<object> AddProjectCommand { get; }

        private void OnAddProject(object obj)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof (CreateProjectView).FullName);
        }

        protected override void OnViewLoaded()
        {
        }
    }
}