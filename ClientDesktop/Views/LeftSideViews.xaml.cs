using System.ComponentModel.Composition;
using System.Windows.Controls;
using ClientDesktop.ViewModels;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for LeftSideViews.xaml
    /// </summary>
    [Export]
    public partial class LeftSideViews : UserControl
    {
        public LeftSideViews()
        {
            InitializeComponent();
        }

        [Import]
        public LeftSideViewModels ViewModel
        {
            set { DataContext = value; }
        }
    }
}