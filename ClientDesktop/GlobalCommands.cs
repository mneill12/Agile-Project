using System.Collections.Generic;
using CSC3045.Agile.Client.Entities;
using Microsoft.Practices.Prism.Commands;

namespace ClientDesktop
{
    internal class GlobalCommands
    {
        public static Account MyAccount;

        public static IList<StoryTask> MyOwnedTasks; 

        public static CompositeCommand NavigateCommand = new CompositeCommand();

        public static CompositeCommand IsLoggedIn = new CompositeCommand();
    }
}