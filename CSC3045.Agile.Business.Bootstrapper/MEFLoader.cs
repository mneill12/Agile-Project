﻿using System.ComponentModel.Composition.Hosting;
using CSC3045.Agile.Business.Business_Engines;
using CSC3045.Agile.Data.Data_Repositories;

namespace CSC3045.Agile.Business.Bootstrapper
{
    // MEF needs to discover assemblies for repository nd engine DI so add one of each to bootstrap assemblies.
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof (AccountRepository).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof (AccountEngine).Assembly));

            var container = new CompositionContainer(catalog);

            return container;
        }
    }
}