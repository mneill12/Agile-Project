using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Business_Engines;
using CSC3045.Agile.Client.Proxies;
using CSC3045.Agile.Data.Data_Repositories;
using Prism.Mef;

namespace ClientDesktop
{
    // Prism MEF Bootstrapper
    // TODO: Use Client MEFLoader instead of this bootstrapper
    class ApplicationBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        // Adds MEF DI Import support for local assemblies + service assemblies from CSC3045.Agile.Client.Proxies
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AccountClient).Assembly));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
