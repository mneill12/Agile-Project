using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Business.Services;
using System.Collections;

namespace CSC3045.Agile.Business.Services.Tests
{
    [TestClass]
    public class ProjectServicesTests
    {
        private readonly Account projectManagerAccount = new Account()
        {
            AccountId = 1,
            FirstName = "Test",
            LastName = "Account1",
            LoginEmail = "test@account.com",
            UserRoles = new HashSet<UserRole>()
            {
                new UserRole()
                {
                    UserRoleName = "Project Manager"
                }
            }
        };

        private readonly Account productOwnerAccount = new Account()
        {
            AccountId = 2,
            FirstName = "Test",
            LastName = "Account2",
            LoginEmail = "test@account.com",
            UserRoles = new HashSet<UserRole>()
            {
                new UserRole()
                {
                    UserRoleName = "Product Owner"
                }
            }
        };

        [TestMethod]
        public void test_add_project()
        {
            Project projectToAdd = new Project();

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Add(projectToAdd)).Returns(projectToAdd);

            ProjectService service = new ProjectService(mockDataRepositoryFactory.Object);

            Project returnedProject = service.AddProject(projectToAdd);

            Assert.IsTrue(returnedProject != null);

        }

        [TestMethod]
        public void test_get_project_info()
        {
            Project projectToGet = new Project() { ProjectId = 45, ProductOwner = productOwnerAccount, ProjectManager = projectManagerAccount, ProjectName = "testtprojectt" };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().GetByProjectId(45)).Returns(projectToGet);

            ProjectService service = new ProjectService(mockDataRepositoryFactory.Object);

            Project project = service.GetProjectInfo(45);

            Assert.IsTrue(project == projectToGet);
        }

        [TestMethod]
        public void test_get_projects_by_project_manager()
        {
            IEnumerable<Project> projectsToGet = new List<Project>()
            {
                new Project() { ProjectId = 1, ProductOwner = productOwnerAccount, ProjectManager = projectManagerAccount, ProjectName = "testtprojectt1" },
                new Project() { ProjectId = 2, ProductOwner = productOwnerAccount, ProjectManager = projectManagerAccount, ProjectName = "testtprojectt2" }
            };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().GetManagedProjectsByAccount(1)).Returns(projectsToGet);

            ProjectService service = new ProjectService(mockDataRepositoryFactory.Object);

            IEnumerable<Project> projects = service.GetProjectsByProjectManager(1);

            Assert.IsTrue(projects == projectsToGet);
        }

        [TestMethod]
        public void test_get_projects_by_account()
        {
            

            ICollection<Account> associatedAccounts1 = new HashSet<Account>()
            {
                new Account() {AccountId=100},
                new Account() {AccountId=101},
                new Account() {AccountId=102}
            };

            ICollection<Account> associatedAccounts2 = new HashSet<Account>()
            {
                new Account() {AccountId=200},
                new Account() {AccountId=201},
                new Account() {AccountId=202}
            };

            IEnumerable<Project> projectsToGet = new List<Project>()
            {
                new Project() { ProjectId = 1, ProductOwner = new Account(){AccountId = 1}, ProjectManager = new Account(){AccountId = 12}, ProjectName = "testproject1", AssociatedUsers = associatedAccounts1 },
                new Project() { ProjectId = 2, ProductOwner = new Account(){AccountId = 2}, ProjectManager = new Account(){AccountId = 12}, ProjectName = "testproject2", AssociatedUsers = associatedAccounts2 }
            };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().GetProjectsByAccount(200)).Returns(projectsToGet);

            ProjectService service = new ProjectService(mockDataRepositoryFactory.Object);

            IEnumerable<Project> projects = service.GetProjectsByAccount(200);

            Assert.IsTrue(projects == projectsToGet);
        }
    }
}
