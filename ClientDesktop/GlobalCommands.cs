using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Client.Entities;
using Microsoft.Practices.Prism.Commands;

namespace ClientDesktop
{
    class GlobalCommands
    {
        public static Account MyAccount;

        public static CompositeCommand IsLoggedIn = new CompositeCommand();

    }
}
