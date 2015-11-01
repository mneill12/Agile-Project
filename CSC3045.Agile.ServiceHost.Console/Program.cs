using System;
using System.Collections.Generic;
using Core.Common.Core;
using CSC3045.Agile.Business.Bootstrapper;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Business.Services;
using System.Data.Entity;
using System.Linq;
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

                DbSet userStorySet = db.UserStorySet;

                foreach (UserStory userStoryTest in userStorySet)
                {
           
                    System.Console.WriteLine(
                        "Testing populated UserStory returns associated tasks and acceptance criteria with one DB call");
                    System.Console.WriteLine(
                        "=============================================================================================");
                    System.Console.WriteLine("Story Number: " + userStoryTest.StoryNumber);
                    System.Console.WriteLine("Title: " + userStoryTest.Title);
                    System.Console.WriteLine("Description: " + userStoryTest.Description);
                    System.Console.WriteLine("Story Points: " + userStoryTest.StoryPoints);
                    System.Console.WriteLine("Status: " + userStoryTest.Status.StoryStatusName);
                    System.Console.WriteLine("Tasks: " + userStoryTest.AssociatedTasks);

                    foreach (StoryTask tsk in userStoryTest.AssociatedTasks)
                    {
                        System.Console.WriteLine("\t ===============================================");
                        System.Console.WriteLine("\t Title: " + tsk.Title);
                        System.Console.WriteLine("\t Description: " + tsk.Description);
                        System.Console.WriteLine("\t Status: " + tsk.CurrentStatus.StoryStatusName);
                        System.Console.WriteLine("\t Hours: " + tsk.Hours);
                        System.Console.WriteLine("\t Blocked Status: " + tsk.IsBlocked);
                    }

                    System.Console.WriteLine("Acceptance Critera: ");

                    foreach (AcceptanceCriteria ac in userStoryTest.AcceptanceCriteria)
                    {
                        System.Console.WriteLine("\t ===============================================");
                        System.Console.WriteLine("\t Scenario: " + ac.Scenario);

                        foreach (String cri in ac.Criteria)
                        {
                            System.Console.WriteLine(cri);
                        }

                        System.Console.WriteLine("\t Satisfied?: " + ac.IsSatisfied);
                    }
                }

            }

        }

        static ICollection<UserStory> getUserStories()
        {
            using (var db = new Csc3045AgileContext())
            {
                return db.UserStorySet.Include(s => s.Status).ToList();
            }
        } 

    }
}
