using Core.Common.Core;
using CSC3045.Agile.Business.Bootstrapper;
using CSC3045.Agile.Business.Services;

namespace CSC3045.Agile.ServiceHost.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init MEF to use DI with engines/repositories
            ObjectBase.Container = MEFLoader.Init();

            System.Console.WriteLine("Starting up services...");
            System.Console.WriteLine("");

            System.ServiceModel.ServiceHost hostAccountService = new System.ServiceModel.ServiceHost(typeof(AccountService));
            StartService(hostAccountService, "AccountManager");

            System.ServiceModel.ServiceHost hostAuthenticationService = new System.ServiceModel.ServiceHost(typeof(AuthenticationService));
            StartService(hostAuthenticationService, "AccountManager");

            System.Console.WriteLine("");
            System.Console.WriteLine("Press [Enter] to exit.");
            System.Console.ReadLine();

            StopService(hostAuthenticationService, "AccountManager");
        }

        static void StartService(System.ServiceModel.ServiceHost host, string serviceDescription)
        {
            host.Open();
            System.Console.WriteLine("Service {0} started.", serviceDescription);

            foreach (var endpoint in host.Description.Endpoints)
            {
                System.Console.WriteLine(string.Format("Listening on endpoint:"));
                System.Console.WriteLine(string.Format("Address: {0}", endpoint.Address.Uri));
                System.Console.WriteLine(string.Format("Binding: {0}", endpoint.Binding.Name));
                System.Console.WriteLine(string.Format("Contract: {0}", endpoint.Contract.ConfigurationName));
            }

            System.Console.WriteLine();
        }

        static void StopService(System.ServiceModel.ServiceHost host, string serviceDescription)
        {
            host.Close();
            System.Console.WriteLine("Service {0} stopped.", serviceDescription);
        }
    }
}
