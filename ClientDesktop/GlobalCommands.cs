using System;
using System.Collections.Generic;
using System.Web.UI.DataVisualization.Charting;
using CSC3045.Agile.Client.Entities;
using Microsoft.Practices.Prism.Commands;

namespace ClientDesktop
{
    internal class GlobalCommands
    {
        public static Account MyAccount;

        public static String LoadedXMLFilePath;

        public static CompositeCommand IsLoggedIn = new CompositeCommand();

        // Status bar text
        public static CompositeCommand ApplicationStatusText = new CompositeCommand();
        public static CompositeCommand ConnectionStatusText = new CompositeCommand();
    }
}