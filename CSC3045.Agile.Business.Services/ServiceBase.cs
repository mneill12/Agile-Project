using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using CSC3045.Agile.Business.Entities;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Business.Services
{
    // Base service class used to create an exception wrapper to make sure no exceptions get caught in the proxy layer of the WCF Client
    public class ServiceBase
    {
        public ServiceBase()
        {
            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
