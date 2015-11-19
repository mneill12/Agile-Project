using System.Collections.Generic;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CSC3045.Agile.Business.Services.Tests
{
    [TestClass]
    public class ProjectServicesTests
    {
        private readonly Account productOwnerAccount = new Account
        {
            AccountId = 2,
            FirstName = "Test",
            LastName = "Account2",
            LoginEmail = "test@account.com",
            UserRoles = new HashSet<UserRole>
            {
                new UserRole
                {
                    UserRoleName = "Product Owner"
                }
            }
        };

        private readonly Account projectManagerAccount = new Account
        {
            AccountId = 1,
            FirstName = "Test",
            LastName = "Account1",
            LoginEmail = "test@account.com",
            UserRoles = new HashSet<UserRole>
            {
                new UserRole
                {
                    UserRoleName = "Project Manager"
                }
            }
        };

        [TestMethod]
        public void test_add_project()
        {
            var projectToAdd = new Project();

            var mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Add(projectToAdd))
                .Returns(projectToAdd);

            var service = new ProjectService(mockDataRepositoryFactory.Object);

            var returnedProject = service.CreateProject(projectToAdd);

            Assert.IsTrue(returnedProject != null);
        }

        [TestMethod]
        public void test_get_project_info()
        {
            var projectToGet = new Project
            {
                ProjectId = 45,
                ProductOwner = productOwnerAccount,
                ProjectManager = projectManagerAccount,
                ProjectName = "testtprojectt"
            };

            var mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Get(45))
                .Returns(projectToGet);

            var service = new ProjectService(mockDataRepositoryFactory.Object);

            var project = service.GetProjectInfo(45);

            Assert.IsTrue(project == projectToGet);
        }

        [TestMethod]
        public void test_get_projects_by_project_manager()
        {
            ICollection<Project> projectsToGet = new List<Project>
            {
                new Project
                {
                    ProjectId = 1,
                    ProductOwner = productOwnerAccount,
                    ProjectManager = projectManagerAccount,
                    ProjectName = "testtprojectt1"
                },
                new Project
                {
                    ProjectId = 2,
                    ProductOwner = productOwnerAccount,
                    ProjectManager = projectManagerAccount,
                    ProjectName = "testtprojectt2"
                }
            };

            var mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(
                mock => mock.GetDataRepository<IProjectRepository>().GetProjectsForProjectManager(1))
                .Returns(projectsToGet);

            var service = new ProjectService(mockDataRepositoryFactory.Object);

            var projects = service.GetProjectsForProjectManager(1);

            Assert.IsTrue(projects == projectsToGet);
        }

        [TestMethod]
        public void test_get_projects_by_account()
        {
            ICollection<Account> allUserAccounts1 = new HashSet<Account>
            {
                new Account {AccountId = 100},
                new Account {AccountId = 101},
                new Account {AccountId = 102}
            };

            ICollection<Account> allUserAccounts2 = new HashSet<Account>
            {
                new Account {AccountId = 200},
                new Account {AccountId = 201},
                new Account {AccountId = 202}
            };

            ICollection<Project> projectsToGet = new List<Project>
            {
                new Project
                {
                    ProjectId = 1,
                    ProductOwner = new Account {AccountId = 1},
                    ProjectManager = new Account {AccountId = 12},
                    ProjectName = "testproject1",
                    AllUsers = allUserAccounts1
                },
                new Project
                {
                    ProjectId = 2,
                    ProductOwner = new Account {AccountId = 2},
                    ProjectManager = new Account {AccountId = 12},
                    ProjectName = "testproject2",
                    AllUsers = allUserAccounts2
                }
            };

            var mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(
                mock => mock.GetDataRepository<IProjectRepository>().GetProjectsForAccount(200)).Returns(projectsToGet);

            var service = new ProjectService(mockDataRepositoryFactory.Object);

            var projects = service.GetProjectsForAccount(200);

            Assert.IsTrue(projects.Equals(projectsToGet));
        }
    }
}