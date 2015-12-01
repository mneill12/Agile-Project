using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for TaskDashboardView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TaskDashboardView : UserControlViewBase
    {
        public TaskDashboardView()
        {
            InitializeComponent();
        }

        [Import]
        public TaskDashboardViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }


}