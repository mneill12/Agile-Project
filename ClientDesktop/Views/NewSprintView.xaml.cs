using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for NewSprintView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class NewSprintView : UserControlViewBase
    {
        public NewSprintView()
        {
            InitializeComponent();
        }

        [Import]
        public NewSprintViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }


}