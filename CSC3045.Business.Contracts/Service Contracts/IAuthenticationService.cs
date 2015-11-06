using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Core.Common;
using Core.Common.Exceptions;
using Core.Common.Utils;
using CSC3045.Agile.Business.Entities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Account AuthenticateUser(string email, string Password);
    }
}

