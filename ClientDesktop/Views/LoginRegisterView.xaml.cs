using System;
using System.Collections.Generic;
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
using Core.Common.Core;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    /// Interaction logic for LoginRegisterView.xaml
    /// </summary>
    public partial class LoginRegisterView : UserControlViewBase    
    {
        public LoginRegisterView()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (false)
            {
                this.DataContext = ObjectBase.Container.GetExportedValue<DashboardViewModel>();
            }
            else
            {
                //Alert user login has failed
                MessageBox.Show("Failed login");
            }
        }
    }
}
