using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using Core.Common.Exceptions;
using CSC3045.Agile.Business.Entities;
using CSC3045.Agile.Business.Entities.XMLEntities;

namespace CSC3045.Agile.Business.Contracts
{
    [ServiceContract]
    public interface IXMLSerialisationService
    {
        [OperationContract]
        [FaultContract(typeof (NotFoundException))]
        String SerialiseProject(Project project);

    }
}