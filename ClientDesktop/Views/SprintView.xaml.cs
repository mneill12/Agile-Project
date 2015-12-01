using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for SprintView.xaml
    /// </summary> 
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SprintView : UserControlViewBase
    {

        public SprintView()
        {
            InitializeComponent();
        }



        [Import]
        public SprintViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
