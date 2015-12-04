using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for CreatePlanningPokerSessionView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CreatePlanningPokerSessionView : UserControlViewBase
    {
        public CreatePlanningPokerSessionView()
        {
            InitializeComponent();
        }

        [Import]
        public CreatePlanningPokerSessionView ViewModel
        {
            set { DataContext = value; }
        }
    }
}