using System;
using System.Reflection;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap;

namespace MvcPluginMasterApp.PluginsBase
{
    public interface IPlugin
    {
        EfBootstrapper GetEfBootstrapper();
        MenuItem GetMenuItem(RequestContext requestContext);
        void RegisterBundles(BundleCollection bundles);
        void RegisterRoutes(RouteCollection routes);
        void RegisterServices(IContainer container);
    }

    public class EfBootstrapper
    {
        /// <summary>
        /// Assemblies containing EntityTypeConfiguration classes.
        /// </summary>
        public Assembly[] ConfigurationsAssemblies { get; set; }

        /// <summary>
        /// Domain classes.
        /// </summary>
        public Type[] DomainEntities { get; set; }

        /// <summary>
        /// Custom Seed method.
        /// </summary>
        public Action<IUnitOfWork> DatabaseSeeder { get; set; }
    }

    public class MenuItem
    {
        public string Name { set; get; }
        public string Url { set; get; }
    }
}