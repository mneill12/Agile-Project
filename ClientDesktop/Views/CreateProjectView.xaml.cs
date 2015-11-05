using ClientDesktop.ViewModels;
using Core.Common.UI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for CreateProjectView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CreateProjectView : UserControlViewBase
    {
        public CreateProjectView()
        {
            InitializeComponent();
        }

        [Import]
        public CreateProjectViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
