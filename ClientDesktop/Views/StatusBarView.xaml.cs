using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for StatusBarView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class StatusBarView : UserControlViewBase
    {
        public StatusBarView()
        {
            InitializeComponent();
        }

        [Import]
        public StatusBarViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}