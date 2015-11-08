using Core.Common.Contracts;
using Core.Common.Core;
using CSC3045.Agile.Client.Bootstrapper;
using CSC3045.Agile.Client.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var proxy
                = ObjectBase.Container.GetExportedValue<IAccountService>();

            Assert.IsTrue(proxy is IAccountService);
        }

        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            IServiceFactory factory = new ServiceFactory();
            var proxy = factory.CreateClient<IAccountService>();

            Assert.IsTrue(proxy is AccountClient);
        }

        [TestMethod]
        public void obtain_service_factory_and_proxy()
        {
            var factory =
                ObjectBase.Container.GetExportedValue<IServiceFactory>();

            var proxy = factory.CreateClient<IAccountService>();

            Assert.IsTrue(proxy is AccountClient);
        }
    }
}