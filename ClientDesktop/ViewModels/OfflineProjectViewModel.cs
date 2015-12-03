using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Serialization;
using ClientDesktop.Views;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;
using CSC3045.Agile.Client.Entities.XMLEntities;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;

namespace ClientDesktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OfflineProjectViewModel : ViewModelBase
    {
        #region OfflineProjectView Bindings
        public const String nbsp = " ";

        public XMLProject Project { get; set; }
        public String ProductOwner { get; set; }
        public String ProjectManager { get; set; }
        public String ProjectStartDate { get; set; }
        public String ProjectSavedDate { get; set; }
        public List<String> DeveloperList { get; set; }
        public List<String> ScrummasterList { get; set; }
        public List<XMLSprint> SprintList { get; set; }
            #endregion

        IServiceFactory _ServiceFactory;
        IRegionManager _RegionManager;

        [ImportingConstructor]
        public OfflineProjectViewModel(IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _ServiceFactory = serviceFactory;
            _RegionManager = regionManager;

        }

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        protected override void OnViewLoaded()
        {
            XMLProject loadedProject = LoadProject(GlobalCommands.LoadedXMLFilePath);
            ScrummasterList = new List<string>();
            DeveloperList = new List<string>();
            SprintList = new List<XMLSprint>();

            Project = loadedProject;
            ProductOwner = loadedProject.ProductOwner.FirstName + nbsp + loadedProject.ProductOwner.LastName;
            ProjectManager = loadedProject.ProjectManager.FirstName + nbsp + loadedProject.ProjectManager.LastName;
            ProjectStartDate = String.Format("{0:M/d/yyyy}", loadedProject.ProjectStartDate);
            ProjectSavedDate = String.Format("{0:M/d/yyyy}", loadedProject.ProjectSavedDate);
            SprintList = loadedProject.Sprints;

            foreach (XMLAccount user in loadedProject.Developers)
            {
                DeveloperList.Add(user.FirstName + nbsp + user.LastName);
            }

            foreach (XMLAccount user in loadedProject.ScrumMasters)
            {
                ScrummasterList.Add(user.FirstName + nbsp + user.LastName);
            }

        }

        public XMLProject LoadProject(string serialisedProjectFilePath)
        {
            if (string.IsNullOrEmpty(serialisedProjectFilePath)) { return default(XMLProject); }

            XMLProject deserialisedProject = default(XMLProject);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(serialisedProjectFilePath);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(XMLProject);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        deserialisedProject = (XMLProject)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error loading XML document: " + ex.Message);
                MessageBox.Show(ex.InnerException.ToString(), "Oops!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return deserialisedProject;
        }
        
    }
}