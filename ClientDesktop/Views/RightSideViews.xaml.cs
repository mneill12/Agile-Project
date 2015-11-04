using System.ComponentModel.Composition;
using System.Windows.Controls;
using ClientDesktop.ViewModels;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for RightSideViews.xaml
    /// </summary>
    [Export]
    public partial class RightSideViews : UserControl
    {
        public RightSideViews()
        {
            InitializeComponent();
        }

        [Import]
        public RightSideViewModels ViewModel
        {
            set { DataContext = value; }
        }
    }
}