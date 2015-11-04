using System.ComponentModel.Composition;
using System.Windows.Controls;
using ClientDesktop.ViewModels;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for MainViews.xaml
    /// </summary>
    [Export]
    public partial class MainViews : UserControlViewBase
    { 
        public MainViews()
        {
            InitializeComponent();
        }

        [Import]
        public MainViewModels ViewModel
        {
            set { DataContext = value; }
        }
    }
}