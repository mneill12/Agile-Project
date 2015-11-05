using System.ComponentModel.Composition;
using System.Windows.Controls;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardViewModel : ViewModelBase, INavigationAware
    {
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
