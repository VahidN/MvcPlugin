using System.Linq;
using System.Web.Optimization;
using System.Web.Routing;
using MvcPluginMasterApp;
using MvcPluginMasterApp.IoCConfig;
using MvcPluginMasterApp.PluginsBase;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(PluginsStart), "Start")]

namespace MvcPluginMasterApp
{
    public static class PluginsStart
    {
        public static void Start()
        {
            var plugins = SmObjectFactory.Container.GetAllInstances<IPlugin>().ToList();
            foreach (var plugin in plugins)
            {
                plugin.RegisterServices(SmObjectFactory.Container);
                plugin.RegisterRoutes(RouteTable.Routes);
                plugin.RegisterBundles(BundleTable.Bundles);
            }
        }
    }
}