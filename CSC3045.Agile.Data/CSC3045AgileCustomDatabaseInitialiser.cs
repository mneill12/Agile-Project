using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Data
{
    internal class Csc3045AgileCustomDatabaseInitialiser : DropCreateDatabaseIfModelChanges<Csc3045AgileContext>
    {
        protected override void Seed(Csc3045AgileContext context)
        {

            Dictionary<DateTime, int> hoursDictionary1 = new Dictionary<DateTime, int>();
            hoursDictionary1.Add( new DateTime(2015, 10, 5), 10);
            hoursDictionary1.Add( new DateTime(2015, 10, 6), 9);
            hoursDictionary1.Add( new DateTime(2015, 10, 7), 8);
            hoursDictionary1.Add( new DateTime(2015, 10, 8), 7);
            hoursDictionary1.Add( new DateTime(2015, 10, 9), 6);

            hoursDictionary1.Add( new DateTime(2015, 10, 12), 5);
            hoursDictionary1.Add( new DateTime(2015, 10, 13), 4);
            hoursDictionary1.Add( new DateTime(2015, 10, 14), 4);
            hoursDictionary1.Add( new DateTime(2015, 10, 15), 4);
            hoursDictionary1.Add( new DateTime(2015, 10, 16), 1);

            TaskBurndownPoint tbp1 = new TaskBurndownPoint()
            {
                HoursRemaining = hoursDictionary1
            };

            Dictionary<DateTime, int> hoursDictionary2 = new Dictionary<DateTime, int>();
            hoursDictionary2.Add( new DateTime(2015, 10, 5), 8);
            hoursDictionary2.Add( new DateTime(2015, 10, 6), 8);
            hoursDictionary2.Add( new DateTime(2015, 10, 7), 8);
            hoursDictionary2.Add( new DateTime(2015, 10, 8), 7);
            hoursDictionary2.Add( new DateTime(2015, 10, 9), 6);

            hoursDictionary2.Add( new DateTime(2015, 10, 12), 5);
            hoursDictionary2.Add( new DateTime(2015, 10, 13), 4);
            hoursDictionary2.Add( new DateTime(2015, 10, 14), 4);
            hoursDictionary2.Add( new DateTime(2015, 10, 15), 4);
            hoursDictionary2.Add( new DateTime(2015, 10, 16), 1);

            TaskBurndownPoint tbp2 = new TaskBurndownPoint()
            {
                HoursRemaining = hoursDictionary2
            };

            Dictionary<DateTime, int> hoursDictionary3 = new Dictionary<DateTime, int>();
            hoursDictionary3.Add( new DateTime(2015, 10, 5), 12);
            hoursDictionary3.Add( new DateTime(2015, 10, 6), 10);
            hoursDictionary3.Add( new DateTime(2015, 10, 7), 8);
            hoursDictionary3.Add( new DateTime(2015, 10, 8), 7);
            hoursDictionary3.Add( new DateTime(2015, 10, 9), 6);

            hoursDictionary3.Add( new DateTime(2015, 10, 12), 5);
            hoursDictionary3.Add( new DateTime(2015, 10, 13), 4);
            hoursDictionary3.Add( new DateTime(2015, 10, 14), 4);
            hoursDictionary3.Add( new DateTime(2015, 10, 15), 4);
            hoursDictionary3.Add( new DateTime(2015, 10, 16), 1);

            TaskBurndownPoint tbp3 = new TaskBurndownPoint()
            {
                HoursRemaining = hoursDictionary3
            };

            IList<TaskBurndownPoint> taskBurndownPoint1 = new List<TaskBurndownPoint>();

            taskBurndownPoint1.Add(new TaskBurndownPoint
            {
                TaskBurndownPointId = 1,
                StoryTaskId = 1,
                HoursRemaining = hoursDictionary1,            
 
            });

             IList<TaskBurndownPoint> taskBurndownPoint2 = new List<TaskBurndownPoint>();

            taskBurndownPoint2.Add(new TaskBurndownPoint
            {
                TaskBurndownPointId = 2,
                StoryTaskId = 1,
                HoursRemaining = hoursDictionary2,            
 
            });

            IList<TaskBurndownPoint> taskBurndownPoint3 = new List<TaskBurndownPoint>();

            taskBurndownPoint3.Add(new TaskBurndownPoint
            {
                TaskBurndownPointId = 3,
                StoryTaskId = 1,
                HoursRemaining = hoursDictionary3,            
 
            });

            IList<CurrentStatus> statuesList = new List<CurrentStatus>();

            statuesList.Add(new CurrentStatus{CurrentStatusName = "To-Do"});
            statuesList.Add(new CurrentStatus { CurrentStatusName = "In Progress" });
            statuesList.Add(new CurrentStatus { CurrentStatusName = "QA" });
            statuesList.Add(new CurrentStatus { CurrentStatusName = "Done" });

            context.CurrentStatusSet.AddRange(statuesList);

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

            var developerUserRoleSetWithScrumMasterAndProductOwner = new HashSet<UserRole>
            {
                defaultRoles[0],
                defaultRoles[1],
                defaultRoles[2]
            };

            IList<Account> defaultAccounts = new List<Account>(); // Plaintext password: 4nt1t7!

            defaultAccounts.Add(new Account
            {
                LoginEmail = "jflyn07n@qub.ac.uk",
                Password = "kYt8nwSk+rRitvhmseNKSrjyB06QKHlrQljre3t8O9I=",
                FirstName = "Joe",
                LastName = "Flynn",
                UserRoles = developerUserRoleSetWithScrumMasterAndProductOwner
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
                ProjectName = "Project 1",
                ProjectStartDate = new DateTime(2015, 11, 11),
                ScrumMasters = new List<Account>() { },
                Developers = new List<Account>() {  },
                AllUsers = new List<Account>() { },
                BacklogStories = new List<UserStory>() { }
            });

            defaultProjects.Add(new Project
            {
                ProjectName = "Project 2",
                ProjectStartDate = new DateTime(2015, 12, 11),
                ScrumMasters = new List<Account>() { },
                Developers = new List<Account>() { },
                AllUsers = new List<Account>() { },
                BacklogStories = new List<UserStory>() { }
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
                    StoryNumber = "P1B1Story",
                    Description = "Project1Backlog1Story",
                    StoryPoints = 15,
                    CurrentStatus = context.CurrentStatusSet.Local[0]
                },
                new UserStory
                {
                    StoryNumber = "P1B2Story",
                    Description = "Project1Backlog1Story",
                    StoryPoints = 35,
                    CurrentStatus = context.CurrentStatusSet.Local[0]
                },
                   
                new UserStory
                {
                    StoryNumber = "P2B1Story",
                    Description = "Project2Backlog1Story",
                    StoryPoints = 24,
                    CurrentStatus = context.CurrentStatusSet.Local[0]
                },
                new UserStory
                {
                    StoryNumber = "P2B2Story",
                    Description = "Project2Backlog2Story",
                    StoryPoints = 14,
                    CurrentStatus = context.CurrentStatusSet.Local[0]
                },

                new UserStory
                {
                    CurrentStatus = context.CurrentStatusSet.Local[1],
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
                            Hours = 10,
                            CurrentStatus = context.CurrentStatusSet.Local[0],
                            IsBlocked = false,
                            TaskBurndownPoint = tbp1
                        },
                        new StoryTask
                        {
                            Title = "TSK-002",
                            Description = "Develop server application to accept client connections for user management.",
                            Hours = 8,
                            CurrentStatus = context.CurrentStatusSet.Local[1],
                            IsBlocked = false,
                            TaskBurndownPoint = tbp2
                        },
                        new StoryTask
                        {
                            Title = "TSK-003",
                            Description =
                                "Develop client application to make connection to server and call database CRUD methods.",
                            Hours = 12,
                            CurrentStatus = context.CurrentStatusSet.Local[2],
                            //TaskBurndownPoint = new TaskBurndownPoint{ TaskBurndownPointId = };
                            IsBlocked = false,
                            TaskBurndownPoint = tbp3
                        }/*,
                        new StoryTask
                        {
                            Title = "TSK-004",
                            Description =
                                "Develop encryption strategy and methods to obfuscate usernames and encrypt passwords.",
                            Hours = 9,
                            CurrentStatus = CurrentStatus = context.CurrentStatusSet.Local[3],
                            IsBlocked = false
                        },
                        new StoryTask
                        {
                            Title = "TSK-005",
                            Description = "Develop UI Registration Screen",
                            Hours = 12,
                            CurrentStatus = CurrentStatus = context.CurrentStatusSet.Local[1],
                            IsBlocked = false
                        },
                        new StoryTask
                        {
                            Title = "TSK-006",
                            Description = "Create unit and integration tests for user management operations.",
                            Hours = 14,
                            CurrentStatus = CurrentStatus = context.CurrentStatusSet.Local[3],
                            IsBlocked = false
                        }*/
                    }
                }
            };

            context.UserStorySet.AddRange(defaultUserStories);

            base.Seed(context);
        }
    }
}