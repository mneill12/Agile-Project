using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Windows;
using CSC3045.Agile.Client.Bootstrapper;
using Core.Common.Core;
using CSC3045.Agile.Client.CustomPrinciples;

namespace ClientDesktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Might get this via DI
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>()
            {
                new AssemblyCatalog(Assembly.GetExecutingAssembly())
            });

            
        }
		
    }

}
