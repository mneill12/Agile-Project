using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel;
using System.Data.Entity;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data
{
    class Csc3045AgileCustomDatabaseInitialiser : DropCreateDatabaseIfModelChanges<Csc3045AgileContext>
    {
        protected override void Seed(Csc3045AgileContext context)
        {
            IList<Account> defaultAccounts = new List<Account>();

            var developerUserRoleSet = new HashSet<UserRole>();
            developerUserRoleSet.Add(new UserRole() {UserRoleName = "Developer", PermissionLevel = 0});

            var developerUserRoleSetWithScrumMaster = new HashSet<UserRole>();
            developerUserRoleSetWithScrumMaster.Add(new UserRole() {UserRoleName = "Developer", PermissionLevel = 0});
            developerUserRoleSetWithScrumMaster.Add(new UserRole() {UserRoleName = "Scrum Master", PermissionLevel = 1});

            var developerUserRoleSetWithProductOwner = new HashSet<UserRole>();
            developerUserRoleSetWithProductOwner.Add(new UserRole() { UserRoleName = "Developer", PermissionLevel = 0 });
            developerUserRoleSetWithProductOwner.Add(new UserRole() { UserRoleName = "Product Owner", PermissionLevel = 3 });


            context.AccountSet.Add(new Account()
            {
                LoginEmail = "jflyn07n@qub.ac.uk",
                Password = "kYt8nwSk+rRitvhmseNKSrjyB06QKHlrQljre3t8O9I=", // Plaintext password: 4nt1t7!
                FirstName = "Joe",
                LastName = "Flynn",
                UserRoles = developerUserRoleSet
            });

            context.AccountSet.Add(new Account()
            { 
                LoginEmail = "zeadie01@qub.ac.uk", 
                Password = "4nt1t7!", 
                FirstName = "Zarah", 
                LastName = "Eadie",
                UserRoles = developerUserRoleSet
            });

            context.AccountSet.Add(new Account()
            {
                LoginEmail = "rmeharg01@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Ryan",
                LastName = "Meharg",
                UserRoles = developerUserRoleSet
            });
            context.AccountSet.Add(new Account()
            {
                LoginEmail = "nreid11@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Niall",
                LastName = "Reid",
                UserRoles = developerUserRoleSetWithScrumMaster
            });

            context.AccountSet.Add(new Account()
            {
                LoginEmail = "zshen01@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Gary (Zidong)",
                LastName = "Shen",
                UserRoles = developerUserRoleSet
            });

            context.AccountSet.Add(new Account()
            {
                LoginEmail = "mmcann71@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Martin",
                LastName = "McCann",
                UserRoles = developerUserRoleSetWithProductOwner
            });

            context.AccountSet.Add(new Account()
            {
                LoginEmail = "mneil12@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Matthew",
                LastName = "Neil",
                UserRoles = developerUserRoleSetWithScrumMaster
            });

            foreach (Account acc in defaultAccounts)
            {
                context.AccountSet.Add(acc);
            }

            IList<Project> defaultProjects = new List<Project>();

            defaultProjects.Add(new Project()
            {
                ProjectManager = new Account() { AccountId = 1 },
                ProductOwner = new Account() { AccountId = 11 },
                ProjectName = "TestProject1",
                ProjectDeadline = new DateTime(2015, 1, 1)

            });

            defaultProjects.Add(new Project()
            {
                ProjectManager = new Account(){AccountId = 2},
                ProductOwner = new Account(){AccountId = 12},
                ProjectName = "TestProject2",
                ProjectDeadline = new DateTime(2015, 1, 1)
            });

            foreach (Project project in defaultProjects)
            {
                context.ProjectSet.Add(project);
            }



            IList<UserRole> defaultRoles = new List<UserRole>();

            defaultRoles.Add(new UserRole()
            {
                UserRoleName = "Project Manager", 
                PermissionLevel = 2
            });

            defaultRoles.Add(new UserRole()
            {
                UserRoleName = "Product Owner", 
                PermissionLevel = 3
            });

            defaultRoles.Add(new UserRole()
            {
                UserRoleName = "Scrum Master",
                PermissionLevel = 1
            });

            foreach (UserRole usr in defaultRoles) 
            { 
                context.UserRoleSet.Add(usr);
            }

            UserStory defaultUserStory = new UserStory()

            {
                Status = new CurrentStatus()
                {
                    StoryStatusName = "Ready for Development"
                },
                Description = "As a user of the scrum client program I can register so that I can connect to the scrum management server",
                StoryNumber = "CSC-001",
                StoryPoints = 13,
                AcceptanceCriteria = new HashSet<AcceptanceCriteria>()
                {
                    new AcceptanceCriteria()
                    {
                        Scenario = "An unregistered user can open the client executable, start to create an account with their email address and password and have it verified as acceptable.",
                        Criteria = new HashSet<Criteria>()
                             {
                                 new Criteria() {CriteriaType =  "GIVEN", CriteriaOutline = "The user is not already registered and the user has the client application" } ,
                                 new Criteria() {CriteriaType =  "WHEN", CriteriaOutline = "The user opens the client application and starts the registration process" },
                                 new Criteria() {CriteriaType =  "THEN", CriteriaOutline = "The user can create an account successfully" }
                             },
                        IsSatisfied = false
                    }
                },
                AssociatedTasks = new HashSet<StoryTask>()
                {
                    new StoryTask()
                    {
                        Title = "TSK-001",
                        Description = "Setup database for server application, to include user management tables.",
                        Hours = 6,
                        CurrentStatus = new CurrentStatus() {   StoryStatusName = "To-Do" },
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        Title = "TSK-002",
                        Description = "Develop server application to accept client connections for user management.",
                        Hours = 8,
                        CurrentStatus = new CurrentStatus() {   StoryStatusName = "BA-QA" },
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        Title = "TSK-003",
                        Description = "Develop client application to make connection to server and call database CRUD methods.",
                        Hours = 4,
                        CurrentStatus = new CurrentStatus() {   StoryStatusName = "Tech QA" },
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        Title = "TSK-004",
                        Description = "Develop encryption strategy and methods to obfuscate usernames and encrypt passwords.",
                        Hours = 9,
                        CurrentStatus = new CurrentStatus() {   StoryStatusName = "Done" },
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        Title = "TSK-005",
                        Description = "Develop UI Registration Screen",
                        Hours = 12,
                        CurrentStatus = new CurrentStatus() {   StoryStatusName = "User Acceptance Testing" },
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        Title = "TSK-006",
                        Description = "Create unit and integration tests for user management operations.",
                        Hours = 14,
                        CurrentStatus = new CurrentStatus() {   StoryStatusName = "Done" },
                        IsBlocked = false,
                    }
                }
                
            };

            context.UserStorySet.Add(defaultUserStory);

            base.Seed(context);
        }
    }
}
