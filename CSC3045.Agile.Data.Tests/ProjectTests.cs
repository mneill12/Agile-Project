using Core.Common.Core;
using CSC3045.Agile.Business.Bootstrapper;
using CSC3045.Agile.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using CSC3045.Agile.Business.Services;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Data;

namespace CSC3045.Agile.Data.Tests
{
    [TestClass]
    public class ProjectTests
    {

        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_get_project_by_id()
        {
            ProjectService pj = new ProjectService();

            Project project = pj.GetProjectInfo(1);

            Assert.IsTrue(project != null);

        }

        [TestMethod]
        public void test_addentity()
        {
            ProjectService service = new ProjectService();

            Project returnedProject = service.AddProject(new Project { ProjectManager = new Account() { AccountId = 4 }, ProductOwner = new Account() { AccountId = 5 }, ProjectName = "TestProject100", ProjectDeadline = new DateTime(2016, 1, 1) });

            Assert.IsTrue(returnedProject != null);
        }

        [TestMethod]
        public void test_get_managed_projects_by_account()
        {
            ProjectService service = new ProjectService();

            IEnumerable<Project> project = service.GetProjectsByProjectManager(1);

            Assert.IsTrue(project.Count() > 0);
        }

        [TestMethod]
        public void test_get_owned_projects_by_account()
        {
            ProjectService service = new ProjectService();

            IEnumerable<Project> project = service.GetProjectsByProductOwner(11);

            Assert.IsTrue(project.Count() > 0);
        }

        [TestMethod]
        public void get_projects_by_account()
        {
            ProjectService service = new ProjectService();

            IEnumerable<Project> project = service.GetProjectsByAccount(100);

            Assert.IsTrue(project.Count() > 0);
        }
    }
}
