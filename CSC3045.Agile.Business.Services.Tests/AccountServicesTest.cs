using System.ServiceModel;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Entities;
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
            var accountToGet = new Account();

            var mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(
                mock => mock.GetDataRepository<IAccountRepository>().GetByLogin("test@example.com"))
                .Returns(accountToGet);

            var service = new AccountService(mockDataRepositoryFactory.Object);

            var account = service.GetAccountInfo("test@example.com");

            Assert.IsTrue(account == accountToGet);
        }

        [TestMethod]
        [ExpectedException(typeof (FaultException))]
        public void test_account_not_found()
        {
            var mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();

            new AccountService(mockDataRepositoryFactory.Object).GetAccountInfo("test@example.com");
        }
    }
}