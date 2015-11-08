using System.ComponentModel.Composition.Hosting;
using System.Windows;
using CSC3045.Agile.Client.Proxies;
using Prism.Mef;

namespace ClientDesktop
{
    // Prism MEF Bootstrapper
    // TODO: Use Client MEFLoader instead of this bootstrapper
    internal class ApplicationBootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        // Adds MEF DI Import support for local assemblies + service assemblies from CSC3045.Agile.Client.Proxies
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (AccountClient).Assembly));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell) Shell;
            Application.Current.MainWindow.Show();
        }
    }
}