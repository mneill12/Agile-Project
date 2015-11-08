using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.Core;
using CSC3045.Agile.Business.Bootstrapper;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data.Contracts.Repository_Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CSC3045.Agile.Data.Tests
{
    [TestClass]
    public class DataLayerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        // Integration test for repositories
        [TestMethod]
        public void test_repository_usage()
        {
            var repositioryTest = new RepositoryTestClass();

            var accounts = repositioryTest.GetAccounts();

            Assert.IsTrue(accounts != null);
        }

        // Integration test for factories
        [TestMethod]
        public void test_repository_factory_usage()
        {
            var factoryTest = new RepositoryFactoryTestClass();

            var accounts = factoryTest.GetAccounts();

            Assert.IsTrue(accounts != null);
        }

        [TestMethod]
        public void test_factory_mocking()
        {
            var accounts = new List<Account>
            {
                new Account {AccountId = 1, LoginEmail = "joebrown@example.com"},
                new Account {AccountId = 2, LoginEmail = "wendyspoon@example.com"}
            };

            var mockDataRespoitory = new Mock<IDataRepositoryFactory>();
            mockDataRespoitory.Setup(obj => obj.GetDataRepository<IAccountRepository>().Get()).Returns(accounts);

            var factoryTest = new RepositoryFactoryTestClass(mockDataRespoitory.Object);

            var returned = factoryTest.GetAccounts();

            Assert.IsTrue(returned == accounts);
        }

        // Unit test mocking IAccountRepository
        [TestMethod]
        public void test_repository_mocking()
        {
            var accounts = new List<Account>
            {
                new Account {AccountId = 1, LoginEmail = "joebrown@example.com"},
                new Account {AccountId = 2, LoginEmail = "wendyspoon@example.com"}
            };

            var mockAccountRespoitory = new Mock<IAccountRepository>();
            mockAccountRespoitory.Setup(obj => obj.Get()).Returns(accounts);

            var repositioryTest = new RepositoryTestClass(mockAccountRespoitory.Object);

            var returned = repositioryTest.GetAccounts();

            Assert.IsTrue(returned == accounts);
        }
    }

    public class RepositoryTestClass
    {
        [Import] private IAccountRepository _AccountRepository;

        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(IAccountRepository accountRepository)
        {
            _AccountRepository = accountRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            var accounts = _AccountRepository.Get();

            return accounts;
        }
    }


    public class RepositoryFactoryTestClass
    {
        [Import] private IDataRepositoryFactory _DataRepositoryFactory;

        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        public IEnumerable<Account> GetAccounts()
        {
            var accountRepository =
                _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            var accounts = accountRepository.Get();

            return accounts;
        }
    }
}