using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Common.Core;
using CSC3045.Agile.Business.Bootstrapper;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Business.Services;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Core.Common.Contracts;
using Core.Common.Extensions;
using CSC3045.Agile.Data;
using CSC3045.Agile.Data.Contracts;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;


namespace CSC3045.Agile.ServiceHost.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init MEF to use DI with engines/repositories
            ObjectBase.Container = MEFLoader.Init();

            System.Console.WriteLine("Starting up services...");
            System.Console.WriteLine("");

            System.ServiceModel.ServiceHost hostAccountService = new System.ServiceModel.ServiceHost(typeof(AccountService));
            StartService(hostAccountService, "AccountService");

            System.ServiceModel.ServiceHost hostAuthenticationService = new System.ServiceModel.ServiceHost(typeof(AuthenticationService));
            StartService(hostAuthenticationService, "AuthenticationService");

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

                    RunDatabaseTests();
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error: {0}", e.Message);
                System.Console.WriteLine("Error: {0}", e.InnerException);
                System.Console.WriteLine("");

            }

            System.Console.WriteLine("Press [Enter] to exit.");
            System.Console.ReadLine();

            StopService(hostAuthenticationService, "AccountService");
            StopService(hostAuthenticationService, "AuthenticationService");
        }

        static void StartService(System.ServiceModel.ServiceHost host, string serviceDescription)
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

        static void StopService(System.ServiceModel.ServiceHost host, string serviceDescription)
        {
            host.Close();
            System.Console.WriteLine("Service {0} stopped.", serviceDescription);
        }

        //TODO: Move account and userrole repository tests out of servicehost
        static void RunDatabaseTests()
        {
            using (new Csc3045AgileContext())
            {

                ICollection<UserStory> userStorySet = GetUserStories();
                IEnumerable<Account> accountSet = GetAccounts(true);

                foreach (UserStory userStoryTest in userStorySet)
                {
                    System.Console.WriteLine("Default user story: \t\t\t\t\t\t" + userStoryTest.StoryNumber);
                    System.Console.WriteLine("Associated Task Count: \t\t\t\t\t\t" + userStoryTest.AssociatedTasks.Count());
                    System.Console.WriteLine("Associated Acceptance Criteria Count: \t\t\t\t" + userStoryTest.AcceptanceCriteria.Count());
                }

                System.Console.WriteLine();
                System.Console.WriteLine("Database checks complete, view complete data? \t\t\t[y = Yes, n = No]");
                var input = System.Console.ReadLine();
                if (null != input && input.Contains("y"))
                {

                    foreach (UserStory userStoryTest in userStorySet)
                    {

                        System.Console.WriteLine(
                            "Testing populated UserStory returns associated tasks and acceptance criteria with one DB call");
                        System.Console.WriteLine(
                            "=============================================================================================");
                        System.Console.WriteLine("Story Number:\t" + userStoryTest.StoryNumber);
                        System.Console.WriteLine("Description:'t" + userStoryTest.Description);
                        System.Console.WriteLine("Story Points:\t" + userStoryTest.StoryPoints);
                        System.Console.WriteLine("Status:\t" + userStoryTest.Status.StoryStatusName);
                        System.Console.WriteLine();
                        System.Console.WriteLine("\t Tasks:");
                        System.Console.WriteLine();

                        System.Console.WriteLine("\t===============================================");
                        foreach (StoryTask tsk in userStoryTest.AssociatedTasks)
                        {
                            System.Console.WriteLine("\tTitle:\t" + tsk.Title);
                            System.Console.WriteLine("\tDescription:\t" + tsk.Description);
                            System.Console.WriteLine("\tHours:\t" + tsk.Hours);
                            System.Console.WriteLine("\tBlocked Status:\t" + tsk.IsBlocked);
                            System.Console.WriteLine("\tStatus: " + tsk.CurrentStatus.StoryStatusName);
                            System.Console.WriteLine("\t===============================================");
                        }

                        System.Console.WriteLine();
                        System.Console.WriteLine("\tAcceptance Critera: ");
                        System.Console.WriteLine();

                        foreach (AcceptanceCriteria ac in userStoryTest.AcceptanceCriteria)
                        {
                            System.Console.WriteLine();
                            System.Console.WriteLine("\tScenario:\t" + ac.Scenario);

                            System.Console.WriteLine();
                            foreach (Criteria cri in ac.Criteria)
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

                    foreach (Account account in accountSet)
                    {
                        System.Console.WriteLine("Full Name: \t\t\t" + account.FirstName + " " + account.LastName);
                        System.Console.WriteLine("Login Email:\t\t\t" + account.LoginEmail);
                        System.Console.WriteLine("Password:\t\t\t" + account.Password);
                        System.Console.WriteLine();
                        System.Console.WriteLine("\tUser Roles:");
                        System.Console.WriteLine();
                        foreach (UserRole role in account.UserRoles)
                        {

                            System.Console.WriteLine("\tRole:\t\t\t\t" + role.UserRoleName);
                            System.Console.WriteLine("\tPermission Level:\t\t" + role.PermissionLevel);
                            System.Console.WriteLine();
                        }

                        System.Console.WriteLine();
                    }
                }
            }
        }

        // Eager loading query to load associated entities when retrieving user stories
        // @todo : move to user story repo if/when CF DB works on all machines
        static ICollection<UserStory> GetUserStories()
        {
            using (var db = new Csc3045AgileContext())
            {
                return db.UserStorySet
                    .Include(s => s.Status)
                    .Include(s => s.AssociatedTasks.Select(p => p.CurrentStatus))
                    .Include(s => s.AcceptanceCriteria.Select(p => p.Criteria)).ToList();
            }
        }

        // Eager loading query to load associated entities when retrieving Accounts
        // @todo : move to account repo if/when CF DB works on all machines
        static ICollection<Account> GetAccounts(bool withUserRoles)
        {
            AccountService service = new AccountService();

            if (withUserRoles)
            {
                return service.GetAllAccountsWithUserRoles();
            }

            return service.GetAllAccounts();
        } 

    }
}
