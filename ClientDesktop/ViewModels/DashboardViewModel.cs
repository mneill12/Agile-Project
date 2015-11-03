using System.ComponentModel.Composition;
using System.Windows.Controls;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public DashboardViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;

            CurrentViewModel = this;
        }

        IServiceFactory _ServiceFactory;

        public override string ViewTitle
        {
            get { return "Dashboard"; }
        }

        protected override void OnViewLoaded()
        {

        }
        
    }
}
