using System;
using System.ServiceModel;
using CSC3045.Agile.Business.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSC3045.Agile.ServiceHost.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
       [TestMethod]
        public void test_account_manager_as_service()
        {
            ChannelFactory<IAccountService> channelFactory =
                new ChannelFactory<IAccountService>("");

            IAccountService proxy = channelFactory.CreateChannel();

            // Just test connectivity to services on a host, no need to make a call to a service
            (proxy as ICommunicationObject).Open();

            channelFactory.Close();
        }
    }
}
