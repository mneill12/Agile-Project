﻿using Core.Common.Contracts;
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
        [TestMethod]
        public void test_add_project()
        {
            Project projectToAdd = new Project();

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Add(projectToAdd)).Returns(projectToAdd);

            ProjectService service = new ProjectService(mockDataRepositoryFactory.Object);

            bool result = service.AddProject(projectToAdd);

            Assert.IsTrue(result, "true");

        }

        [TestMethod]
        public void test_get_project_info()
        {
            Project projectToGet = new Project() { ProjectId = 45, ProductOwnerId = 32, ProjectManagerId = 64, ProjectName = "testtprojectt" };

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
                new Project() { ProjectId = 1, ProductOwnerId = 1, ProjectManagerId = 12, ProjectName = "testtprojectt1" },
                new Project() { ProjectId = 2, ProductOwnerId = 2, ProjectManagerId = 12, ProjectName = "testtprojectt2" }
            };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().GetManagedProjectsByAccount(12)).Returns(projectsToGet);

            ProjectService service = new ProjectService(mockDataRepositoryFactory.Object);

            IEnumerable<Project> projects = service.GetProjectsByProjectManager(12);

            Assert.IsTrue(projects == projectsToGet);
        }
    }
}