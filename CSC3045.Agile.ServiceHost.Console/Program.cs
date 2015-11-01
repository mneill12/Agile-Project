using System;
using System.Collections.Generic;
using Core.Common.Core;
using CSC3045.Agile.Business.Bootstrapper;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Business.Services;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using CSC3045.Agile.Data;

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

            System.ServiceModel.ServiceHost hostAccountManager = new System.ServiceModel.ServiceHost(typeof(AccountService));
            StartService(hostAccountManager, "AccountManager");

            System.Console.WriteLine("");
            System.Console.WriteLine("Press [Enter] to initialize database.");
            System.Console.ReadLine();

            System.Console.WriteLine("Initialising CodeFirst Database");
            try
            {
                InitDb();
                System.Console.WriteLine("Successfully created database.");
                System.Console.WriteLine("Server: (localDb)\v11.0");
                System.Console.WriteLine("Database: CSC3045_Agile_CF");
                System.Console.WriteLine("");
                System.Console.WriteLine("Press [Enter] to run database test.");
                System.Console.ReadLine();

                RunDatabaseTests();

                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine("Database tests complete!");
                System.Console.WriteLine("Press [Enter] to exit.");
                System.Console.ReadLine();

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error: {0}", e.Message);
                System.Console.WriteLine("Error: {0}", e.InnerException);
                System.Console.WriteLine("");
                System.Console.WriteLine("Press [Enter] to exit.");
                System.Console.ReadLine();
            }

            StopService(hostAccountManager, "AccountManager");
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

        static void InitDb()
        {
            using (var db = new Csc3045AgileContext())
            {
                var testAccount = new Account();

                testAccount.AccountId = 99999;
                testAccount.LoginEmail = "default@email.com";
                testAccount.FirstName = "Default";
                testAccount.LastName = "User";

                db.AccountSet.Add(testAccount);
                db.SaveChanges();

                db.AccountSet.Remove(testAccount);
                db.SaveChanges();

            } 
        }

        static void RunDatabaseTests()
        {
            using (var db = new Csc3045AgileContext())
            {

                ICollection<UserStory> userStorySet = GetUserStories();
                ICollection<Account> accountSet = GetAccounts();

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
                        // System.Console.WriteLine("\t Status: " + tsk.CurrentStatus.StoryStatusName);
                        System.Console.WriteLine("\t ===============================================");
                    }

                    System.Console.WriteLine();
                    System.Console.WriteLine("\t Acceptance Critera: ");
                    System.Console.WriteLine();

                    foreach (AcceptanceCriteria ac in userStoryTest.AcceptanceCriteria)
                    {
                        System.Console.WriteLine();
                        System.Console.WriteLine("\t Scenario:\t" + ac.Scenario);

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
        static ICollection<Account> GetAccounts()
        {
            using (var db = new Csc3045AgileContext())
            {
                return db.AccountSet
                    .Include(a => a.UserRoles)
                    .ToList();
            }
        } 

    }
}
