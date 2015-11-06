
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using CSC3045.Agile.Client.Entities;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using Core.Common.Utils;


namespace CSC3045.Agile.Client.Contracts
{
    [ServiceContract]
    public interface IAuthenticationService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account AuthenticateUser(string email, string Password);
    }
}
