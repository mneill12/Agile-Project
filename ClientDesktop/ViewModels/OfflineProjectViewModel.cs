using System;
using System.Collections.Generic;
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

        private XMLProject _Project;
       
        public XMLProject Project
        {
            get
            {
                return _Project;
            }
            set
            {
                if (_Project == value) return;
                _Project = value;
                OnPropertyChanged("Project");
            }
        }


        #endregion

        IServiceFactory _ServiceFactory;
        IRegionManager _RegionManager;

        public DelegateCommand<object> CreateProjectCommand { get; set; }
        public DelegateCommand<object> ManageProjectBacklogCommand { get; set; } 
        public DelegateCommand<object> RefreshProjectsCommand { get; set; }
        public DelegateCommand<object> ViewBurndownCommand { get; set; }

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
            BindOfflineProject(loadedProject);
        }

        private void BindOfflineProject(XMLProject loadedProject)
        {
            // Logic to bind info goes here...... later.
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