using System.ServiceModel;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IAuthenticationService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof (NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account AuthenticateUser(string email, string Password);
    }
}