using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mef;

namespace ClientDesktop
{
    // Prism MEF Bootstrapper
    class ApplicationBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
