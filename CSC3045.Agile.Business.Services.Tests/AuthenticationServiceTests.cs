using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using Core.Common.Contracts;
using CSC3045.Agile.Business.Contracts;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CSC3045.Agile.Business.Services.Tests
{
    [TestClass]
    public class AuthentiationServiceTest
    {
        /*
        [TestMethod]
        public void test_authenticateUser()
        {
            var user = new Account();

            var mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(
                mock => mock.GetDataRepository<IAccountRepository>().GetByLoginAndPassword("jflyn07n@qub.ac.uk", "4nt1t7!"))
                .Returns(user);

            var service = new AuthenticationService(mockDataRepositoryFactory.Object);

            var account = service.GetAccountInfo("test@example.com");

            Assert.IsTrue(account == accountToGet);
        }

         */
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void test_account_not_found()
        {
            var mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();

            new AccountService(mockDataRepositoryFactory.Object).GetAccountInfo("test@example.com");
        }
    }
}
