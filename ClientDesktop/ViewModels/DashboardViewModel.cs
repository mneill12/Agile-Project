using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardViewModel : ViewModelBase
    {
        #region LoginRegisterView Bindings

        private ICollection<StoryTask> _OwnedTasks;

        #endregion

        IServiceFactory _ServiceFactory;

        [ImportingConstructor]
        public DashboardViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
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
