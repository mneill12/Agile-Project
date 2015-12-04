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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for ManageSprintView.xaml
    /// </summary>
    /// 
    /// 
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ManageSprintView :UserControlViewBase
    {
        public ManageSprintView()
        {
            InitializeComponent();
        }

        [Import]
        public ManageSprintViewModel ViewModel
        {
            set { DataContext = value; }
        }


    }
}
