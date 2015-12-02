using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Common;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Business.Entities.XMLEntities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;

namespace CSC3045.Agile.Business.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class XMLSerialisationService : ServiceBase, IXMLSerialisationService
    {
        [Import] private IBusinessEngineFactory _BusinessEngineFactory;

        [Import] private IDataRepositoryFactory _DataRepositoryFactory;

        public XMLSerialisationService()
        {
        }

        /// <summary>
        /// Used for testing, this service has the data repository initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        public XMLSerialisationService(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        /// <summary>
        /// Used for testing, this service has the business engine initialized through dependency injection
        /// </summary>
        /// <param name="businessEngineFactory"></param>
        public XMLSerialisationService(IBusinessEngineFactory businessEngineFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
        }

        /// <summary>
        /// Used for testing, this service has the data repository & business engine initialized through dependency injection
        /// </summary>
        /// <param name="dataRepositoryFactory"></param>
        /// <param name="businessEngineFactory"></param>
        public XMLSerialisationService(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
        }

        public String SerialiseProject(Project project)
        {
            if (project == null) { return "Supplied parameter is null!"; }

            XMLProject xmlProject = RemapProjectEntity(project);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(xmlProject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, xmlProject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(xmlProject.ProjectName + "_XML");

                    stream.Close();
                }

                return xmlDocument.BaseURI;
            }
            catch (Exception ex)
            {
                return "Error creating XML Document: " + ex.GetBaseException();
            }

        }

        private XMLProject RemapProjectEntity(Project project)
        {
            XMLProject xmlProject = new XMLProject();

            // Remap simple types
            xmlProject.ProjectName = project.ProjectName;
            xmlProject.ProjectStartDate = project.ProjectStartDate;

            // Remap single complex types
            xmlProject.ProductOwner = RemapAccountEntity(project.ProductOwner);
            xmlProject.ProjectManager = RemapAccountEntity(project.ProjectManager);
            xmlProject.Backlog = RemapBacklogEntity(project.Backlog);

            
            // Remap collections of account types
            foreach (Account scrumMaster in project.ScrumMasters)
            {
                xmlProject.ScrumMasters.Add(RemapAccountEntity(scrumMaster));
            }

            foreach (Account developer in project.Developers)
            {
                xmlProject.Developers.Add(RemapAccountEntity(developer));
            }

            foreach (Account user in project.AllUsers)
            {
                xmlProject.AllUsers.Add(RemapAccountEntity(user));
            }

            foreach (Sprint sprint in project.Sprints)
            {
                xmlProject.Sprints.Add(RemapSprintEntity(sprint));
            }

            foreach (Burndown burndown in project.Burndowns)
            {
                xmlProject.Burndowns.Add(RemapBurndownEntity(burndown));
            }

        
            return xmlProject;
        }

        private XMLAccount RemapAccountEntity(Account account)
        {
            XMLAccount xmlAccount = new XMLAccount();

            // Remap simple types
            xmlAccount.FirstName = account.FirstName;
            xmlAccount.LastName = account.LastName;
            xmlAccount.LoginEmail = account.LoginEmail;
            xmlAccount.Password = account.Password;


            return xmlAccount;
        }

        private XMLBacklog RemapBacklogEntity(Backlog backlog)
        {
            XMLBacklog xmlBacklog = new XMLBacklog();

            return xmlBacklog;
        }

        private XMLAcceptanceCriteria RemapAcceptanceCriteriaEntity(AcceptanceCriteria acceptanceCriteria)
        {
            XMLAcceptanceCriteria xmlAcceptanceCriteria = new XMLAcceptanceCriteria();

            return xmlAcceptanceCriteria;
        }

        private XMLBurndown RemapBurndownEntity(Burndown burndown)
        {
            XMLBurndown xmlBurndown = new XMLBurndown();

            return xmlBurndown;
        }

        private XMLSprint RemapSprintEntity(Sprint sprint)
        {
            XMLSprint xmlSprint = new XMLSprint();

            return xmlSprint;

        }

        private XMLUserStory RemapUserStoryEntity(UserStory userStory)
        {
            XMLUserStory xmlUserStory = new XMLUserStory();

            return xmlUserStory;
        }

        public Project LoadProject(string serialisedProjectFilePath)
        {
            if (string.IsNullOrEmpty(serialisedProjectFilePath)) { return default(Project); }

            Project deserialisedProject = default(Project);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(serialisedProjectFilePath);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(Project);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        deserialisedProject = (Project)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
               Console.Out.WriteLine("Error loading XML document.");

            }

            return deserialisedProject;
        }
    }
}