using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for ScrumMasterView.xaml
    /// </summary> 
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ScrumMasterView : UserControlViewBase
    {

        public ScrumMasterView()
        {
            InitializeComponent();
        }



        [Import]
        public LoginRegisterViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
