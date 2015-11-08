using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Windows;
using Core.Common.Core;
using CSC3045.Agile.Client.Bootstrapper;
using CSC3045.Agile.Client.CustomPrinciples;

namespace ClientDesktop
{
    // Calls Prism MEF Bootstrapper on application run
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            // Init proxies for ServiceFactorty.cs
            ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>
            {
                new AssemblyCatalog(Assembly.GetExecutingAssembly())
            });

            // Init proxies for services in ViewModels
            var bootstrapper = new ApplicationBootstrapper();
            bootstrapper.Run();
        }
    }
}