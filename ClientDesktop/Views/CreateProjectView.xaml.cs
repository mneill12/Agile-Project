using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for CreateProjectView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CreateProjectView : UserControlViewBase
    {
        public CreateProjectView()
        {
            InitializeComponent();
        }

        [Import]
        public CreateProjectViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}