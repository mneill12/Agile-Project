using System.ComponentModel.Composition;
using System.Windows.Controls;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for ProductBacklogManagementView.xaml
    /// </summary>

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ProductBacklogManagementView : UserControlViewBase
    {
        public ProductBacklogManagementView()
        {
            InitializeComponent();
        }

        [Import]
        public ProductBacklogManagementViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
