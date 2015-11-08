using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for TopBarView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TopBarView : UserControlViewBase
    {
        public TopBarView()
        {
            InitializeComponent();
        }

        [Import]
        public TopBarViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}