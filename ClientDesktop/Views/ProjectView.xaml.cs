using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for EditAccountView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ProjectView : UserControlViewBase
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        [Import]
        public ProjectViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}