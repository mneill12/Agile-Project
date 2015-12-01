using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for TaskListView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TaskListView : UserControlViewBase
    {
        public TaskListView()
        {
            InitializeComponent();
        }

        [Import]
        public TaskListViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }


}