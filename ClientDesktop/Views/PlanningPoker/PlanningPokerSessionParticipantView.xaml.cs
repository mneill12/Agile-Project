using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for PlanningPokerSessionParticipantView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PlanningPokerSessionParticipantView : UserControlViewBase
    {
        public PlanningPokerSessionParticipantView()
        {
            InitializeComponent();
        }

        [Import]
        public PlanningPokerSessionParticipantView ViewModel
        {
            set { DataContext = value; }
        }
    }
}