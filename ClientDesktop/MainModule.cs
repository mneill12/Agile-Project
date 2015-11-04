using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace ClientDesktop
{
    // Collection of view and view models wrapped in a Prism module
    [Module(ModuleName = "MainModule")]
    [ModuleExport(typeof(MainModule))]
    public class MainModule : IModule
    {
        private IRegionManager _RegionManager;
   
        [ImportingConstructor]
        public MainModule(IRegionManager regionManager)
        {
            this._RegionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            _RegionManager.RegisterViewWithRegion("TopBarRegion", typeof (Views.TopBarViews));
            _RegionManager.RegisterViewWithRegion("MainRegion", typeof(Views.MainViews));
        }

        #endregion

    }

}