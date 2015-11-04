using System.ComponentModel.Composition;
using System.Windows.Controls;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for TopBarViews.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TopBarViews : UserControlViewBase
    {
        public TopBarViews()
        {
            InitializeComponent();
        }

        [Import]
        public TopBarViewModels ViewModel
        {
            set { DataContext = value; }
        }
    }
}