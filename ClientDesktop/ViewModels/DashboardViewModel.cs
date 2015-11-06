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
    public class DashboardViewModel : ViewModelBase
    {
        private readonly DelegateCommand<object> _AddProjectCommand;

        public DelegateCommand<object> AddProjectCommand { get { return _AddProjectCommand; } }

        IServiceFactory _ServiceFactory;
        IRegionManager _RegionManager;

        [ImportingConstructor]
        public DashboardViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

            _AddProjectCommand = new DelegateCommand<object>(OnAddProject);
        }

        private void OnAddProject(object obj)
        {
            _RegionManager.RequestNavigate(RegionNames.Content, typeof(CreateProjectView).FullName);
        }


        public override string ViewTitle
        {
            get { return "Dashboard"; }
        }

        protected override void OnViewLoaded()
        {

        }
    }
}
