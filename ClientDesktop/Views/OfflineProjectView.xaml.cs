using System.ComponentModel.Composition;
using System.Windows;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for OfflineProjectView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class OfflineProjectView : UserControlViewBase
    {
        public OfflineProjectView()
        {
            InitializeComponent();
        }

        [Import]
        public OfflineProjectViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}