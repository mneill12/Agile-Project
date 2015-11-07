using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using ClientDesktop.ViewModels;
using ClientDesktop.Views;
using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace ClientDesktop
{
    // Collection of view and view models wrapped in a Prism module
    [Module(ModuleName = "MainModule")]
    [ModuleExport(typeof(MainModule))]
    public class MainModule : ObjectBase, IModule
    {
        private IRegionManager _RegionManager;

        [ImportingConstructor]
        public MainModule(IRegionManager regionManager)
        {
            this._RegionManager = regionManager;
        }

        #region IModule Members

        // Using MEF DI for adding views to a region that has been setup in shell
        public void Initialize()
        {
            _RegionManager.Regions[RegionNames.TopBar].Add(ServiceLocator.Current.GetInstance<TopBarView>());
            _RegionManager.Regions[RegionNames.Content].Add(ServiceLocator.Current.GetInstance<LoginRegisterView>());
        }

        #endregion

    }

}