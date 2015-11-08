using System;
using System.Collections.Generic;
using System.Data.Entity;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data
{
    internal class Csc3045AgileCustomDatabaseInitialiser : DropCreateDatabaseIfModelChanges<Csc3045AgileContext>
    {
        protected override void Seed(Csc3045AgileContext context)
        {
            IList<UserRole> defaultRoles = new List<UserRole>();

            defaultRoles.Add(new UserRole
            {
                EntityId = 1,
                UserRoleId = 1,
                UserRoleName = "Developer"
            });

            defaultRoles.Add(new UserRole
            {
                EntityId = 2,
                UserRoleId = 2,
                UserRoleName = "Product Owner"
            });

            defaultRoles.Add(new UserRole
            {
                EntityId = 3,
                UserRoleId = 3,
                UserRoleName = "Scrum Master"
            });

            context.UserRoleSet.AddRange(defaultRoles);


            var developerUserRoleSet = new HashSet<UserRole>
            {
                defaultRoles[0]
            };

            var developerUserRoleSetWithProductOwner = new HashSet<UserRole>
            {
                defaultRoles[0],
                defaultRoles[1]
            };

            var developerUserRoleSetWithScrumMaster = new HashSet<UserRole>
            {
                defaultRoles[0],
                defaultRoles[2]
            };

            IList<Account> defaultAccounts = new List<Account>(); // Plaintext password: 4nt1t7!

            defaultAccounts.Add(new Account
            {
                LoginEmail = "jflyn07n@qub.ac.uk",
                Password = "kYt8nwSk+rRitvhmseNKSrjyB06QKHlrQljre3t8O9I=",
                FirstName = "Joe",
                LastName = "Flynn",
                UserRoles = developerUserRoleSet
            });

            defaultAccounts.Add(new Account
            {
                LoginEmail = "zeadie01@qub.ac.uk",
                Password = "y1Mf4YbVbO8M4U3oYFF5toJQoxMQ1wRpJudTWgRflkA=",
                FirstName = "Zarah",
                LastName = "Eadie",
                UserRoles = developerUserRoleSet
            });

            defaultAccounts.Add(new Account
            {
                LoginEmail = "rmeharg01@qub.ac.uk",
                Password = "I2oo4byxivpwtHEbjP9J/kG1KSH+ONqESSkUOJVG0pw=",
                FirstName = "Ryan",
                LastName = "Meharg",
                UserRoles = developerUserRoleSet
            });

            defaultAccounts.Add(new Account
            {
                LoginEmail = "nreid11@qub.ac.uk",
                Password = "N5FdrdwDBMerDk7l0Py9HAk53csbTwvh7sR/+lRUC9A=",
                FirstName = "Niall",
                LastName = "Reid",
                UserRoles = developerUserRoleSetWithScrumMaster
            });

            defaultAccounts.Add(new Account
            {
                LoginEmail = "zshen01@qub.ac.uk",
                Password = "f44ERC/3p4fi036cgRdcpc8/ka1NHu5rom/cBEpXctc=",
                FirstName = "Gary (Zidong)",
                LastName = "Shen",
                UserRoles = developerUserRoleSet
            });

            defaultAccounts.Add(new Account
            {
                LoginEmail = "mmcann71@qub.ac.uk",
                Password = "d4/hmnKHEI74x0K5rxue+lEcKW3WisXAti4yiJT6KpQ=",
                FirstName = "Martin",
                LastName = "McCann",
                UserRoles = developerUserRoleSetWithProductOwner
            });

            defaultAccounts.Add(new Account
            {
                LoginEmail = "mneil12@qub.ac.uk",
                Password = "3WaMnz2fZFJv5Ek2ikmYtws5je/EVLg0cV8F93dFHyU=",
                FirstName = "Matthew",
                LastName = "Neil",
                UserRoles = developerUserRoleSetWithScrumMaster
            });

            context.AccountSet.AddRange(defaultAccounts);

            IList<Project> defaultProjects = new List<Project>();

            defaultProjects.Add(new Project
            {
                ProjectManager = context.AccountSet.Local[0],
                ProductOwner = context.AccountSet.Local[2],
                ProjectName = "Project 1",
                ProjectDeadline = new DateTime(2015, 1, 1)
            });

            defaultProjects.Add(new Project
            {
                ProjectManager = context.AccountSet.Local[3],
                ProductOwner = context.AccountSet.Local[4],
                ProjectName = "Project 2",
                ProjectDeadline = new DateTime(2015, 1, 1)
            });

            foreach (var project in defaultProjects)
            {
                context.ProjectSet.Add(project);
            }

            context.ProjectSet.AddRange(defaultProjects);

            IList<UserStory> defaultUserStories = new List<UserStory>
            {
                new UserStory
                {
                    Status = new CurrentStatus
                    {
                        StoryStatusName = "Ready for Development"
                    },
                    Description =
                        "As a user of the scrum client program I can register so that I can connect to the scrum management server",
                    StoryNumber = "CSC-001",
                    StoryPoints = 13,
                    AcceptanceCriteria = new HashSet<AcceptanceCriteria>
                    {
                        new AcceptanceCriteria
                        {
                            Scenario =
                                "An unregistered user can open the client executable, start to create an account with their email address and password and have it verified as acceptable.",
                            Criteria = new HashSet<Criteria>
                            {
                                new Criteria
                                {
                                    CriteriaType = "GIVEN",
                                    CriteriaOutline =
                                        "The user is not already registered and the user has the client application"
                                },
                                new Criteria
                                {
                                    CriteriaType = "WHEN",
                                    CriteriaOutline =
                                        "The user opens the client application and starts the registration process"
                                },
                                new Criteria
                                {
                                    CriteriaType = "THEN",
                                    CriteriaOutline = "The user can create an account successfully"
                                }
                            },
                            IsSatisfied = false
                        }
                    },
                    AssociatedTasks = new HashSet<StoryTask>
                    {
                        new StoryTask
                        {
                            Title = "TSK-001",
                            Description = "Setup database for server application, to include user management tables.",
                            Hours = 6,
                            CurrentStatus = new CurrentStatus {StoryStatusName = "To-Do"},
                            IsBlocked = false
                        },
                        new StoryTask
                        {
                            Title = "TSK-002",
                            Description = "Develop server application to accept client connections for user management.",
                            Hours = 8,
                            CurrentStatus = new CurrentStatus {StoryStatusName = "BA-QA"},
                            IsBlocked = false
                        },
                        new StoryTask
                        {
                            Title = "TSK-003",
                            Description =
                                "Develop client application to make connection to server and call database CRUD methods.",
                            Hours = 4,
                            CurrentStatus = new CurrentStatus {StoryStatusName = "Tech QA"},
                            IsBlocked = false
                        },
                        new StoryTask
                        {
                            Title = "TSK-004",
                            Description =
                                "Develop encryption strategy and methods to obfuscate usernames and encrypt passwords.",
                            Hours = 9,
                            CurrentStatus = new CurrentStatus {StoryStatusName = "Done"},
                            IsBlocked = false
                        },
                        new StoryTask
                        {
                            Title = "TSK-005",
                            Description = "Develop UI Registration Screen",
                            Hours = 12,
                            CurrentStatus = new CurrentStatus {StoryStatusName = "User Acceptance Testing"},
                            IsBlocked = false
                        },
                        new StoryTask
                        {
                            Title = "TSK-006",
                            Description = "Create unit and integration tests for user management operations.",
                            Hours = 14,
                            CurrentStatus = new CurrentStatus {StoryStatusName = "Done"},
                            IsBlocked = false
                        }
                    }
                }
            };

            context.UserStorySet.AddRange(defaultUserStories);

            base.Seed(context);
        }
    }
}