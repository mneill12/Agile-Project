using System.ComponentModel.Composition;
using System.Windows;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for DashboardView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DashboardView : UserControlViewBase
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        [Import]
        public DashboardViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}