using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
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
using Prism.Modularity;
using Prism.Regions;

namespace ClientDesktop
{
    /// <summary>
    /// Interaction logic for Shell.xaml, replaces the standard MainWindow of WPF applications
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
        private const string MainModuleName = "MainModule";
        private static Uri MainViewUri = new Uri("/MainView", UriKind.Relative);

        public Shell()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public IModuleManager ModuleManager;

        [Import(AllowRecomposition = false)]
        public IRegionManager RegionManager;

        public void OnImportsSatisfied()
        {
            this.ModuleManager.LoadModuleCompleted +=
                (s, e) =>
                {
                    if (e.ModuleInfo.ModuleName == MainModuleName)
                    {
                        this.RegionManager.RequestNavigate("MainRegion", MainViewUri);
                    }
                };
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
