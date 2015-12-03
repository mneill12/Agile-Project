using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Documents;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Entities.XMLEntities;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for OfflineProjectView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class OfflineProjectView : UserControlViewBase
    {
        public OfflineProjectView()
        {
            InitializeComponent();
        }

        [Import]
        public OfflineProjectViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}