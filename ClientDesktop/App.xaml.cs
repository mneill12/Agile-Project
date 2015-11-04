using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace ClientDesktop
{
    // Calls Prism MEF Bootstrapper on application run
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplicationBootstrapper bootstrapper = new ApplicationBootstrapper();
            bootstrapper.Run();
        }
    }
}
