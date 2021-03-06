﻿using System.ServiceModel;
using Core.Common.Contracts;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSC3045.Agile.Business.Entities;
using Moq;


namespace CSC3045.Agile.Business.Services.Tests
{
    [TestClass]
    public class UserStoryServiceTests
    {
        [TestMethod]
        public void test_get_user_story_info()
        {
            UserStory userStoryToGet = new UserStory();

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IUserStoryRepository>().GetUserStoryById(1)).Returns(userStoryToGet);

            UserStoryService userService = new UserStoryService(mockDataRepositoryFactory.Object);

            UserStory userStory = userService.GetUserStoryById(1);

            Assert.IsTrue(userStory == userStoryToGet);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void test_user_story_not_found()
        {
            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();

            new UserStoryService(mockDataRepositoryFactory.Object).GetUserStoryById(1);
        }

    }
}
