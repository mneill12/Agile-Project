using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Common.UI.Core;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainViewModel : ViewModelBase
    {
        [Import]
        public DashboardViewModel DashboardViewModel { get; private set; }

        [Import]
        public LoginRegisterViewModel LoginRegisterViewModel { get; private set; }

    }
}
