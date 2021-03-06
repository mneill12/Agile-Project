﻿using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Core.Common.Core;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof (IServiceFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ServiceFactory : IServiceFactory
    {
        T IServiceFactory.CreateClient<T>()
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}