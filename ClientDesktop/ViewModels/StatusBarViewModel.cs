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
    public class StatusBarViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public StatusBarViewModel()
        {

        }
    }
}
