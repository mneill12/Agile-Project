using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSC3045.Agile.Client.Proxies.Tests
{
    // Requires service host to be running
    [TestClass]
    public class ServiceAccessTests
    {
        [TestMethod]
        public void test_account_client_connection()
        {
            var proxy = new AccountClient();

            proxy.Open();
        }
    }
}