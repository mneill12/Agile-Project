using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for CreateTaskView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CreateTaskView : UserControlViewBase
    {
        public CreateTaskView()
        {
            InitializeComponent();
        }

        [Import]
        public CreateTaskViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }


}