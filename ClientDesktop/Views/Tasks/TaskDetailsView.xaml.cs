using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for TaskDetailsView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TaskDetailsView : UserControlViewBase
    {
        public TaskDetailsView()
        {
            InitializeComponent();
        }

        [Import]
        public TaskDetailsViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }


}