using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using CSC3045.Agile.Client.Proxies;

namespace CSC3045.Agile.Client.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        // Takes in a list of assemblies from a client to load along with loading the ones we already have for DI
        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AccountClient).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ProjectClient).Assembly));

            if (catalogParts != null)
                foreach (var part in catalogParts)
                    catalog.Catalogs.Add(part);

            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }

    }
}
