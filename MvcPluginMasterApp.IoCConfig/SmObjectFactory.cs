using System;
using System.IO;
using System.Threading;
using System.Web;
using MvcPluginMasterApp.DataLayer.Context;
using MvcPluginMasterApp.PluginsBase;
using MvcPluginMasterApp.Services;
using MvcPluginMasterApp.Services.Contracts;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Web;

namespace MvcPluginMasterApp.IoCConfig
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(cfg =>
            {
                //x.For<IConfigService>().Use<ConfigService>(); == scanner.WithDefaultConventions();

                cfg.For<IUnitOfWork>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<MvcPluginMasterAppContext>();

                cfg.For<ICategoryService>().Use<EfCategoryService>();
                cfg.For<IProductService>().Use<EfProductService>();

                cfg.Scan(scanner =>
                {
                    scanner.AssembliesFromPath(
                        path: Path.Combine(HttpRuntime.AppDomainAppPath, "bin"),
                            // يك اسمبلي نبايد دوبار بارگذاري شود
                        assemblyFilter: assembly =>
                        {
                            return !assembly.FullName.Equals(typeof(IPlugin).Assembly.FullName);
                        });

                    scanner.WithDefaultConventions(); //Connects 'IName' interface to 'Name' class automatically.
                    scanner.AddAllTypesOf<IPlugin>().NameBy(item => item.FullName);
                });
            });
        }
    }
}