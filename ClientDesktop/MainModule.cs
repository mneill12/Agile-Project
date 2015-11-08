using System.ComponentModel.Composition;
using ClientDesktop.Views;
using Core.Common.Core;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace ClientDesktop
{
    // Collection of view and view models wrapped in a Prism module
    [Module(ModuleName = "MainModule")]
    [ModuleExport(typeof (MainModule))]
    public class MainModule : ObjectBase, IModule
    {
        private readonly IRegionManager _RegionManager;

        [ImportingConstructor]
        public MainModule(IRegionManager regionManager)
        {
            _RegionManager = regionManager;
        }

        #region IModule Members

        // Using MEF DI for adding views to a region that has been setup in shell
        public void Initialize()
        {
            _RegionManager.Regions[RegionNames.TopBar].Add(ServiceLocator.Current.GetInstance<TopBarView>());
            _RegionManager.Regions[RegionNames.Content].Add(ServiceLocator.Current.GetInstance<LoginRegisterView>());
            _RegionManager.Regions[RegionNames.Status].Add(ServiceLocator.Current.GetInstance<StatusBarView>());
        }

        #endregion
    }
}