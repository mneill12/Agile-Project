using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Common.Core;
using CSC3045.Agile.Business.Bootstrapper;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Business.Entities.XMLEntities;
using CSC3045.Agile.Business.Services;
using CSC3045.Agile.Data;

namespace CSC3045.Agile.ServiceHost.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Init MEF to use DI with engines/repositories
            ObjectBase.Container = MEFLoader.Init();

            System.Console.WriteLine("Starting up services...");
            System.Console.WriteLine("");

            var hostAccountService = new System.ServiceModel.ServiceHost(typeof (AccountService));
            StartService(hostAccountService, "AccountService");

            var hostAuthenticationService = new System.ServiceModel.ServiceHost(typeof (AuthenticationService));
            StartService(hostAuthenticationService, "AuthenticationService");

            var hostProjectService = new System.ServiceModel.ServiceHost(typeof (ProjectService));
            StartService(hostProjectService, "ProjectService");

            var hostBurndownService = new System.ServiceModel.ServiceHost(typeof (BurndownService));
            StartService(hostBurndownService, "BurndownService");

            var hostSprintService = new System.ServiceModel.ServiceHost(typeof(SprintService));
            StartService(hostSprintService, "SprintService");

            var hostUserStoryService = new System.ServiceModel.ServiceHost(typeof(UserStoryService));
            StartService(hostUserStoryService, "UserStoryService");

            /*var hostStoryTaskService = new System.ServiceModel.ServiceHost(typeof(StoryTaskService));
            StartService(hostStoryTaskService, "StoryTaskService");*/

            System.Console.WriteLine("Initialising CodeFirst Database");

            try
            {
                using (var context = new Csc3045AgileContext())
                {
                    System.Console.WriteLine("Successfully created database.");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Data Source:\t\t" + context.Database.Connection.DataSource);
                    System.Console.WriteLine("Database:\t\tCSC3045GeneratedDB");
                    System.Console.WriteLine("Connection String:\t" + context.Database.Connection.ConnectionString);
                    System.Console.WriteLine("");
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error: {0}", e.Message);
                System.Console.WriteLine("Error: {0}", e.InnerException);
                System.Console.WriteLine("");
            }

            LoadSampleData();

            TestXMLSerialisation();
            
            RunDatabaseTests();
            

            System.Console.WriteLine("Press [Enter] to exit.");
            System.Console.ReadLine();

            StopService(hostAccountService, "AccountService");
            StopService(hostAuthenticationService, "AuthenticationService");
            StopService(hostProjectService, "ProjectService");
            StopService(hostBurndownService, "BurndownService");
            StopService(hostSprintService, "SprintService");
            StopService(hostUserStoryService, "UserStoryService");
            //StopService(hostStoryTaskService, "StoryTaskService");
        }

        private static void StartService(System.ServiceModel.ServiceHost host, string serviceDescription)
        {
            host.Open();
            System.Console.WriteLine("Service {0} started.", serviceDescription);

            foreach (var endpoint in host.Description.Endpoints)
            {
                System.Console.WriteLine("Listening on endpoint:");
                System.Console.WriteLine("Address: {0}", endpoint.Address.Uri);
                System.Console.WriteLine("Binding: {0}", endpoint.Binding.Name);
                System.Console.WriteLine("Contract: {0}", endpoint.Contract.ConfigurationName);
            }

            System.Console.WriteLine();
        }

        private static void StopService(System.ServiceModel.ServiceHost host, string serviceDescription)
        {
            host.Close();
            System.Console.WriteLine("Service {0} stopped.", serviceDescription);
        }

        private static void TestXMLSerialisation()
        {
            ProjectService projectService = new ProjectService();
            XMLSerialisationService xmlSerialisationService = new XMLSerialisationService();

            List<String> fileList = new List<string>();

            System.Console.WriteLine("Testing XML Serialisation for Project 1!");
            var project1 = projectService.GetProjectInfo(1);

            String filePath1 = xmlSerialisationService.SerialiseProject(project1);
            fileList.Add(filePath1);

            System.Console.WriteLine("Path to XML File for project 1: {0}", filePath1);
            System.Console.WriteLine(
                            "=============================================================================================");
            System.Console.WriteLine("Testing XML Serialisation for Project 2!");

            var project2 = projectService.GetProjectInfo(2);

            String filePath2 = xmlSerialisationService.SerialiseProject(project2);
            fileList.Add(filePath2);

            System.Console.WriteLine("Path to XML File for project 2: {0}", filePath2);
            System.Console.WriteLine(
                            "=============================================================================================");
           
            System.Console.WriteLine("Serialisation Tests complete. View XML files? \t\t\t[y = Yes, n = No]");
                var answer = System.Console.ReadLine();
            if (null != answer && answer.Contains("y"))
            
            {
                foreach (String path in fileList)
                {
                    System.Diagnostics.Process.Start("notepad.exe", path);
                }
            }
        }

        private static void LoadSampleData()
        {
            // Load project sample data
            ProjectService projectService = new ProjectService();
            AccountService accountService = new AccountService();
            SprintService sprintService = new SprintService();
            UserStoryService userStoryService = new UserStoryService();

            var project1 = projectService.GetProjectInfo(1);
            var project2 = projectService.GetProjectInfo(2);

            List<Account> accounts = accountService.GetAllAccounts().ToList();

            project1.Sprints.Add(new Sprint()
            {
                SprintName = "Discovery Phase",
                SprintNumber = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Now.AddDays(7)
            });

            project1.Sprints.Add(new Sprint()
            {
                SprintName = "Architecture Implementation",
                SprintNumber = 2,
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(21)
            });

            projectService.SaveProject(project1);

            var sprints = sprintService.GetAllSprints();
             
            var userStories = userStoryService.GetAllUserStories();


            //Check if data has already been loaded before
            if (project1.ProjectManager == null || project2.ProjectManager == null || project1.AllUsers.Count == 0 ||
                project2.AllUsers.Count == 0)
            {
                project1.ProjectManager = accounts[1];
                project1.ProductOwner = accounts[2];
                project1.ScrumMasters = new List<Account>() {accounts[3]};
                project1.Developers = new List<Account>()
                {
                    accounts[4]
                };
                project1.AllUsers = accounts;



                project2.ProjectManager = accounts[6];
                project2.ProductOwner = accounts[4];
                project2.ScrumMasters = new List<Account>() { accounts[3] };
                project2.Developers = new List<Account>()
                {
                    accounts[1],
                    accounts[0]
                };
                project2.AllUsers = accounts;

                projectService.UpdateProjectInfo(project1);
                projectService.UpdateProjectInfo(project2);
            }

            if (project1.BacklogStories == null || project1.BacklogStories.Count == 0)
            {
                project1.BacklogStories = new List<UserStory>()
                {
                    userStories.Single(s => s.UserStoryId == 2),
                    userStories.Single(s => s.UserStoryId == 3)
                };
            }

            if (project2.BacklogStories == null || project2.BacklogStories.Count == 0)
            {
                project2.BacklogStories = new List<UserStory>()
                {
                    userStories.Single(s => s.UserStoryId == 4),
                    userStories.Single(s => s.UserStoryId == 5)
                };
            }

            // End load project sample data

            System.Console.WriteLine("Sprints in project1: " + project1.Sprints.Count());
            System.Console.WriteLine("Sprints in project2: " + project2.Sprints.Count());
        }

        //TODO: Move account and userrole repository tests out of servicehost
        private static void RunDatabaseTests()
        {
            using (new Csc3045AgileContext())
            {
                var userStorySet = GetUserStories();
                IEnumerable<Account> accountSet = GetAccounts(true);

                foreach (var userStoryTest in userStorySet)
                {
                    System.Console.WriteLine("Default user story: \t\t\t\t\t\t" + userStoryTest.StoryNumber);
                    System.Console.WriteLine("Associated Task Count: \t\t\t\t\t\t" +
                                             userStoryTest.AssociatedTasks.Count());
                    System.Console.WriteLine("Associated Acceptance Criteria Count: \t\t\t\t" +
                                             userStoryTest.AcceptanceCriteria.Count());
                }

                System.Console.WriteLine();
                System.Console.WriteLine("Database checks complete, view complete data? \t\t\t[y = Yes, n = No]");
                var input = System.Console.ReadLine();
                if (null != input && input.Contains("y"))
                {
                    foreach (var userStoryTest in userStorySet)
                    {
                        System.Console.WriteLine(
                            "Testing populated UserStory returns associated tasks and acceptance criteria with one DB call");
                        System.Console.WriteLine(
                            "=============================================================================================");
                        System.Console.WriteLine("Story Number:\t" + userStoryTest.StoryNumber);
                        System.Console.WriteLine("Description:'t" + userStoryTest.Description);
                        System.Console.WriteLine("Story Points:\t" + userStoryTest.StoryPoints);
                        System.Console.WriteLine("Status:\t" + userStoryTest.CurrentStatus.CurrentStatusName);
                        System.Console.WriteLine();
                        System.Console.WriteLine("\t Tasks:");
                        System.Console.WriteLine();

                        System.Console.WriteLine("\t===============================================");
                        foreach (var tsk in userStoryTest.AssociatedTasks)
                        {
                            System.Console.WriteLine("\tTitle:\t" + tsk.Title);
                            System.Console.WriteLine("\tDescription:\t" + tsk.Description);
                            System.Console.WriteLine("\tHours:\t" + tsk.Hours);
                            System.Console.WriteLine("\tBlocked Status:\t" + tsk.IsBlocked);
                            System.Console.WriteLine("\tStatus: " + tsk.CurrentStatus.CurrentStatusName);
                            System.Console.WriteLine("\t===============================================");
                        }

                        System.Console.WriteLine();
                        System.Console.WriteLine("\tAcceptance Critera: ");
                        System.Console.WriteLine();

                        foreach (var ac in userStoryTest.AcceptanceCriteria)
                        {
                            System.Console.WriteLine();
                            System.Console.WriteLine("\tScenario:\t" + ac.Scenario);

                            System.Console.WriteLine();
                            foreach (var cri in ac.Criteria)
                            {
                                System.Console.WriteLine("\t " + cri.CriteriaType);
                                System.Console.WriteLine("\t " + cri.CriteriaOutline);
                                System.Console.WriteLine();
                            }

                            System.Console.WriteLine("\t Satisfied?:\t" + ac.IsSatisfied);
                        }
                    }

                    System.Console.WriteLine();
                    System.Console.WriteLine(
                        "=============================================================================================");
                    System.Console.WriteLine(
                        "Testing retrieving Account entities also retrieves associated User Roles for each account");
                    System.Console.WriteLine(
                        "=============================================================================================");
                    System.Console.WriteLine();

                    foreach (var account in accountSet)
                    {
                        System.Console.WriteLine("Full Name: \t\t\t" + account.FirstName + " " + account.LastName);
                        System.Console.WriteLine("Login Email:\t\t\t" + account.LoginEmail);
                        System.Console.WriteLine("Password:\t\t\t" + account.Password);
                        System.Console.Write("User Roles:\t\t\t");
                        foreach (var role in account.UserRoles)
                        {
                            System.Console.Write(role.UserRoleName + " ");
                        }

                        System.Console.WriteLine();
                    }
                }
            }
        }

        // Eager loading query to load associated entities when retrieving user stories
        // @todo : move to user story repo if/when CF DB works on all machines
        private static ICollection<UserStory> GetUserStories()
        {
            using (var db = new Csc3045AgileContext())
            {
                return db.UserStorySet
                    .Include(s => s.CurrentStatus)
                    .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                    .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria)).ToList();
            }
        }

        // Eager loading query to load associated entities when retrieving Accounts
        // @todo : move to account repo if/when CF DB works on all machines
        private static ICollection<Account> GetAccounts(bool withUserRoles)
        {
            var service = new AccountService();

            if (withUserRoles)
            {
                return service.GetAllAccountsWithUserRoles();
            }

            return service.GetAllAccounts();
        }
    }
}