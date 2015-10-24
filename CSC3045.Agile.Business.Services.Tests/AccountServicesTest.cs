using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;
using CSC3045.Agile.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Business.Services;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CSC3045.Agile.Business.Services.Tests
{
    [TestClass]
    public class AccountServiceTests
    {
        [TestMethod]
        public void test_get_account_info()
        {
            Account accountToGet = new Account();

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IAccountRepository>().GetByLogin("test@example.com")).Returns(accountToGet);

            AccountService service = new AccountService(mockDataRepositoryFactory.Object);

            Account account = service.GetAccountInfo("test@example.com");

            Assert.IsTrue(account == accountToGet);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void test_account_not_found()
        {
            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();

            new AccountService(mockDataRepositoryFactory.Object).GetAccountInfo("test@example.com");
        }
        
    }
}
