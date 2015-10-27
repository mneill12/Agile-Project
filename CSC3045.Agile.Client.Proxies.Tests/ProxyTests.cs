using System;
using CSC3045.Agile.Client.Bootstrapper;
using CSC3045.Agile.Client.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using Core.Common.Contracts;

namespace CSC3045.Agile.Client.Proxies.Tests
{
    // Tests that we can obtain the proxy from a client service contract, factory and from container
    [TestClass]
    public class ProxyTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void obtain_proxy_using_service_contract()
        {
            IAccountService proxy
                = ObjectBase.Container.GetExportedValue<IAccountService>();

            Assert.IsTrue(proxy is IAccountService);
        }

        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            IServiceFactory factory = new ServiceFactory();
            IAccountService proxy = factory.CreateClient<IAccountService>();

            Assert.IsTrue(proxy is AccountClient);
        }

        [TestMethod]
        public void obtain_service_factory_and_proxy()
        {
            IServiceFactory factory =
                ObjectBase.Container.GetExportedValue<IServiceFactory>();

            IAccountService proxy = factory.CreateClient<IAccountService>();

            Assert.IsTrue(proxy is AccountClient);
        }
    }
}
