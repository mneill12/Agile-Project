using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for PlanningPokerSessionView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PlanningPokerSessionView : UserControlViewBase
    {
        public PlanningPokerSessionView()
        {
            InitializeComponent();
        }

        [Import]
        public PlanningPokerSessionViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}