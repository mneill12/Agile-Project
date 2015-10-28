using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClientDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow login = new MainWindow();
        private Dashboard dashboard = new Dashboard();

        private void Application_Startup(object sender, StartupEventArgs e){
             Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
             Application.Current.MainWindow = login;

             login.LoginSuccessful += dashboard.StartupLoginWindow;
             login.Show();

    }




}

}
