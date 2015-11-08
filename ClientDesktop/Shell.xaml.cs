using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Navigation;

namespace ClientDesktop
{
    /// <summary>
    ///     Interaction logic for Shell.xaml, replaces the standard MainWindow of WPF applications
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}