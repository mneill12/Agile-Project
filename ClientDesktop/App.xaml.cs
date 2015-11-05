using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Core.Common.Core;
using CSC3045.Agile.Client.Bootstrapper;

namespace ClientDesktop
{
    // Calls Prism MEF Bootstrapper on application run
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
		
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            ApplicationBootstrapper bootstrapper = new ApplicationBootstrapper();
            bootstrapper.Run();
        }
    }
}