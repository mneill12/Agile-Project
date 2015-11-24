
using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;
using Microsoft.Research.DynamicDataDisplay;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for BurndownChartView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SprintBurndownChartView : UserControlViewBase
    {
        public SprintBurndownChartView()
        {
            InitializeComponent();
        }

        [Import]
        public SprintBurndownChartViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}