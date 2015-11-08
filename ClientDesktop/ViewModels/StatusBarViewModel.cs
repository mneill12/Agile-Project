using System.ComponentModel.Composition;
using Core.Common.UI.Core;

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