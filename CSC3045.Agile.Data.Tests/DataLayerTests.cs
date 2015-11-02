using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;
using CSC3045.Agile.Business.Bootstrapper;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Data;
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
            RepositoryTestClass repositioryTest = new RepositoryTestClass();

            IEnumerable<Account> accounts = repositioryTest.GetAccounts();

            Assert.IsTrue(accounts != null);
        }

        // Integration test for factories
        [TestMethod]
        public void test_repository_factory_usage()
        {
            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass();

            IEnumerable<Account> accounts = factoryTest.GetAccounts();

            Assert.IsTrue(accounts != null);
        }

        [TestMethod]
        public void test_factory_mocking()
        {
            List<Account> accounts = new List<Account>()
            {
                new Account() {AccountId = 1, LoginEmail = "joebrown@example.com"},
                new Account() {AccountId = 2, LoginEmail = "wendyspoon@example.com"}
            };

            Mock<IDataRepositoryFactory> mockDataRespoitory = new Mock<IDataRepositoryFactory>();
            mockDataRespoitory.Setup(obj => obj.GetDataRepository<IAccountRepository>().Get()).Returns(accounts);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRespoitory.Object);

            IEnumerable<Account> returned = factoryTest.GetAccounts();

            Assert.IsTrue(returned == accounts);
        }

        // Unit test mocking IAccountRepository
        [TestMethod]
        public void test_repository_mocking()
        {
            List<Account> accounts = new List<Account>()
            {
                new Account() { AccountId = 1, LoginEmail = "joebrown@example.com" },
                new Account() { AccountId = 2, LoginEmail = "wendyspoon@example.com" }
            };

            Mock<IAccountRepository> mockAccountRespoitory = new Mock<IAccountRepository>();
            mockAccountRespoitory.Setup(obj => obj.Get()).Returns(accounts);

            RepositoryTestClass repositioryTest = new RepositoryTestClass(mockAccountRespoitory.Object);

            IEnumerable<Account> returned = repositioryTest.GetAccounts();

            Assert.IsTrue(returned == accounts);
        }
    }

    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(IAccountRepository accountRepository)
        {
            _AccountRepository = accountRepository;
        }

        [Import]
        IAccountRepository _AccountRepository;

        public IEnumerable<Account> GetAccounts()
        {
            IEnumerable<Account> accounts = _AccountRepository.GetAccounts();

            return accounts;
        } 
    }


    public class RepositoryFactoryTestClass
    {
        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        private IDataRepositoryFactory _DataRepositoryFactory;

        public IEnumerable<Account> GetAccounts()
        {
            IAccountRepository accountRepository = 
                _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            IEnumerable<Account> accounts = accountRepository.GetAccounts();

            return accounts;
        } 
    }
}
