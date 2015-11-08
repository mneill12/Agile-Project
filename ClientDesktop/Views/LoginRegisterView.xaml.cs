using System.ComponentModel.Composition;
using ClientDesktop.ViewModels;
using Core.Common.UI.Core;

namespace ClientDesktop.Views
{
    /// <summary>
    ///     Interaction logic for LoginRegisterView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class LoginRegisterView : UserControlViewBase
    {
        public LoginRegisterView()
        {
            InitializeComponent();
        }

        [Import]
        public LoginRegisterViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}