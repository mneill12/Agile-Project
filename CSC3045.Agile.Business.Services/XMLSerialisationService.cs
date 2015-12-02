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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(xmlProject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, xmlProject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(path + "/" + xmlProject.ProjectName + "_XML.xml");
                    stream.Close();
                }

                return path + "/" + xmlProject.ProjectName + "_XML.xml";
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
            xmlProject.ProjectId = project.ProjectId;
            xmlProject.ProjectName = project.ProjectName;
            xmlProject.ProjectStartDate = project.ProjectStartDate;

            // Remap single complex types
            xmlProject.ProductOwner = RemapAccountEntity(project.ProductOwner);
            xmlProject.ProjectManager = RemapAccountEntity(project.ProjectManager);
            xmlProject.Backlog = RemapBacklogEntity(project.Backlog);

            
            // Remap collections of account types
            xmlProject.ScrumMasters = new List<XMLAccount>();
            foreach (Account scrumMaster in project.ScrumMasters)
            {
                xmlProject.ScrumMasters.Add(RemapAccountEntity(scrumMaster));
            }

            xmlProject.Developers = new List<XMLAccount>();
            foreach (Account developer in project.Developers)
            {
                xmlProject.Developers.Add(RemapAccountEntity(developer));
            }

            xmlProject.AllUsers = new List<XMLAccount>();
            foreach (Account user in project.AllUsers)
            {
                xmlProject.AllUsers.Add(RemapAccountEntity(user));
            }

            xmlProject.Sprints = new List<XMLSprint>();
            foreach (Sprint sprint in project.Sprints)
            {
                xmlProject.Sprints.Add(RemapSprintEntity(sprint));
            }

            xmlProject.Burndowns = new List<XMLBurndown>();
            foreach (Burndown burndown in project.Burndowns)
            {
                xmlProject.Burndowns.Add(RemapBurndownEntity(burndown));
            }

        
            return xmlProject;
        }

        private XMLAccount RemapAccountEntity(Account account)
        {
            if (null == account) return null;

            XMLAccount xmlAccount = new XMLAccount
            {
                AccountId = account.AccountId,
                FirstName = account.FirstName,
                LastName = account.LastName,
                LoginEmail = account.LoginEmail
            };

            // Remap collections of complex types
            if (null != account.UserRoles)
            {
                foreach (UserRole role in account.UserRoles)
                {
                    xmlAccount.UserRoles.Add(role);
                }
            }

            if (null == account.Skills) return xmlAccount;
            foreach (Skill skill in account.Skills)
            {
                xmlAccount.Skills.Add(skill);
            }


            return xmlAccount;
        }

        private XMLBacklog RemapBacklogEntity(Backlog backlog)
        {
            
            if (null == backlog) return new XMLBacklog();

            XMLBacklog xmlBacklog = new XMLBacklog {BacklogId = backlog.BacklogId};
            if (null == backlog.UserStories) return xmlBacklog;
            
            foreach (UserStory story in backlog.UserStories)
            {
                xmlBacklog.UserStories.Add(RemapUserStoryEntity(story));
            }


            return xmlBacklog;
        }

        private XMLAcceptanceCriteria RemapAcceptanceCriteriaEntity(AcceptanceCriteria acceptanceCriteria)
        {
            if (null == acceptanceCriteria) return null;

            XMLAcceptanceCriteria xmlAcceptanceCriteria = new XMLAcceptanceCriteria
            {
                AcceptanceCriteriaId = acceptanceCriteria.AcceptanceCriteriaId,
                Scenario = acceptanceCriteria.Scenario,
                IsSatisfied = acceptanceCriteria.IsSatisfied
            };

            if (null == acceptanceCriteria.Criteria) return xmlAcceptanceCriteria;
            foreach (Criteria criteria in acceptanceCriteria.Criteria)
            {
                xmlAcceptanceCriteria.Criteria.Add(criteria);
            }

            return xmlAcceptanceCriteria;
        }

        private XMLBurndown RemapBurndownEntity(Burndown burndown)
        {
            if (null == burndown) return new XMLBurndown();

            XMLBurndown xmlBurndown = new XMLBurndown {BurndownName = burndown.BurndownName, BurndownId = burndown.BurndownId};

            if (null == burndown.BurndownPoints) return xmlBurndown;
            foreach (BurndownPoint burndownPoint in burndown.BurndownPoints)
            {
                xmlBurndown.BurndownPoints.Add(burndownPoint);
            }

            return xmlBurndown;
        }

        private XMLSprint RemapSprintEntity(Sprint sprint)
        {
            if (null == sprint) return new XMLSprint();

            XMLSprint xmlSprint = new XMLSprint
            {
                SprintId = sprint.SprintId,
                SprintName = sprint.SprintName,
                SprintNumber = sprint.SprintNumber,
                EndDate = sprint.EndDate,
                StartDate = sprint.StartDate,
                ScrumMaster = RemapAccountEntity(sprint.ScrumMaster),
                Backlog = RemapBacklogEntity(sprint.Backlog)
            };

            if (null != sprint.Burndowns)
            {
                foreach (Burndown burndown in sprint.Burndowns)
                {
                    xmlSprint.Burndowns.Add(RemapBurndownEntity(burndown));
                }
            }

            if (null == sprint.SprintMembers) return xmlSprint;
            foreach (Account member in sprint.SprintMembers)
            {
                xmlSprint.SprintMembers.Add(RemapAccountEntity(member));
            }

            return xmlSprint;

        }

        private XMLUserStory RemapUserStoryEntity(UserStory userStory)
        {
            if (null == userStory) return new XMLUserStory();

            XMLUserStory xmlUserStory = new XMLUserStory
            {
                UserStoryId = userStory.UserStoryId,
                Description = userStory.Description,
                Status = userStory.Status,
                StoryPoints = userStory.StoryPoints,
                StoryNumber = userStory.StoryNumber,
                UserNotes = userStory.UserNotes
            };

            if (null == userStory.AssociatedTasks) return xmlUserStory;
            foreach (StoryTask task in userStory.AssociatedTasks)
            {
                xmlUserStory.AssociatedTasks.Add(RemapStoryTask(task));
            }

            return xmlUserStory;
        }

        private XMLStoryTask RemapStoryTask(StoryTask task)
        {
            if (null == task) return new XMLStoryTask();

            XMLStoryTask xmlStoryTask = new XMLStoryTask
            {
                StoryTaskId = task.StoryTaskId,
                Description = task.Description,
                CurrentStatus = task.CurrentStatus,
                Hours = task.Hours,
                IsBlocked = task.IsBlocked,
                Title = task.Title,
                UserNotes = task.UserNotes,
                Owner = RemapAccountEntity(task.Owner)
            };

            return xmlStoryTask;
        }

    }
}