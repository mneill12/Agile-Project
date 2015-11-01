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
    class Csc3045AgileCustomDatabaseInitialiser : DropCreateDatabaseAlways<Csc3045AgileContext>
    {
        protected override void Seed(Csc3045AgileContext context)
        {
            IList<Account> defaultAccounts = new List<Account>();

            var developerUserRoleSet = new HashSet<UserRole>();
            developerUserRoleSet.Add(new UserRole() {UserRoleId = 1, UserRoleName = "Developer", PermissionLevel = 0});

            var developerUserRoleSetWithScrumMaster = new HashSet<UserRole>();
            developerUserRoleSetWithScrumMaster.Add(new UserRole() {UserRoleId = 1, UserRoleName = "Developer", PermissionLevel = 0});
            developerUserRoleSetWithScrumMaster.Add(new UserRole() {UserRoleId = 1, UserRoleName = "Scrum Master", PermissionLevel = 1});

            defaultAccounts.Add(new Account()
            {
                AccountId = 1,
                LoginEmail = "jflyn07n@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Joe",
                LastName = "Flynn",
                UserRoles = developerUserRoleSet
            });

            defaultAccounts.Add(new Account()
            {
                AccountId = 2, 
                LoginEmail = "zeadie01@qub.ac.uk", 
                Password = "4nt1t7!", 
                FirstName = "Zarah", 
                LastName = "Eadie",
                UserRoles = developerUserRoleSet
            });
            defaultAccounts.Add(new Account()
            {
                AccountId = 3,
                LoginEmail = "rmeharg01@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Ryan",
                LastName = "Meharg",
                UserRoles = developerUserRoleSet
            });
            defaultAccounts.Add(new Account()
            {
                AccountId = 4,
                LoginEmail = "nreid11@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Niall",
                LastName = "Reid",
                UserRoles = developerUserRoleSetWithScrumMaster
            });
            defaultAccounts.Add(new Account()
            {
                AccountId = 5,
                LoginEmail = "zshen01@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Gary (Zidong)",
                LastName = "Shen",
                UserRoles = developerUserRoleSet
            });
            defaultAccounts.Add(new Account()
            {
                AccountId = 6,
                LoginEmail = "mmcann71@qub.ac.uk",
                Password = "4nt1t7!",
                FirstName = "Martin",
                LastName = "McCann",
                UserRoles = developerUserRoleSetWithScrumMaster
            });
            defaultAccounts.Add(new Account()
            {
                AccountId = 7,
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


            IList<UserRole> defaultRoles = new List<UserRole>();

           
            defaultRoles.Add(new UserRole()
            {
                UserRoleId = 3, 
                UserRoleName = "Project Manager", 
                PermissionLevel = 2
            });

            defaultRoles.Add(new UserRole()
            {
                UserRoleId = 4, 
                UserRoleName = "Product Owner", 
                PermissionLevel = 3
            });


            foreach (UserRole usr in defaultRoles) 
            { 
                context.UserRoleSet.Add(usr);
            }

            UserStory defaultUserStory = new UserStory()

            {
                UserStoryId = 0,
                Status = new StoryStatus()
                {
                    StoryStatusId  = 0, 
                    StoryStatusName = "Ready for Development"
                },
                Description = "As a user of the scrum client program I can register so that I can connect to the scrum management server",
                StoryNumber = "CSC-001",
                StoryPoints = 13,
                AcceptanceCriteria = new HashSet<AcceptanceCriteria>()
                {
                    new AcceptanceCriteria()
                    {
                        AcceptanceCriteriaId = 0,
                        Scenario = "An unregistered user can open the client executable, start to create an account with their email address and password and have it verified as acceptable.",
                        Criteria = new HashSet<String>()
                             {
                                 "GIVEN The user is not already registered and the user has the client application",
                                 "WHEN The user opens the client application and starts the registration process",
                                 "THEN The user can create an account successfully"
                             },
                        IsSatisfied = false
                    }
                },
                AssociatedTasks = new HashSet<StoryTask>()
                {
                    new StoryTask()
                    {
                        StoryTaskId = 0,
                        Title = "TSK-001",
                        Description = "Setup database for server application, to include user management tables.",
                        Hours = 6,
                        CurrentStatus = context.StoryStatusSet.Find(0),
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        StoryTaskId = 1,
                        Title = "TSK-002",
                        Description = "Develop server application to accept client connections for user management.",
                        Hours = 8,
                        CurrentStatus = context.StoryStatusSet.Find(0),
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        StoryTaskId = 2,
                        Title = "TSK-003",
                        Description = "Develop client application to make connection to server and call database CRUD methods.",
                        Hours = 4,
                        CurrentStatus = context.StoryStatusSet.Find(0),
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        StoryTaskId = 3,
                        Title = "TSK-004",
                        Description = "Develop encryption strategy and methods to obfuscate usernames and encrypt passwords.",
                        Hours = 9,
                        CurrentStatus = context.StoryStatusSet.Find(0),
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        StoryTaskId = 4,
                        Title = "TSK-005",
                        Description = "Develop UI Registration Screen",
                        Hours = 12,
                        CurrentStatus = context.StoryStatusSet.Find(0),
                        IsBlocked = false,
                    },
                    new StoryTask()
                    {
                        StoryTaskId = 4,
                        Title = "TSK-006",
                        Description = "Create unit and integration tests for user management operations.",
                        Hours = 14,
                        CurrentStatus = context.StoryStatusSet.Find(0),
                        IsBlocked = false,
                    }
                }
                
            };

            context.UserStorySet.Add(defaultUserStory);

            base.Seed(context);
        }
    }
}
