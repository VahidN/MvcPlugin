using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MvcPluginMasterApp;
using MvcPluginMasterApp.DataLayer.Context;
using MvcPluginMasterApp.IoCConfig;
using MvcPluginMasterApp.PluginsBase;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(EFBootstrapperStart), "Start")]
namespace MvcPluginMasterApp
{
    public static class EFBootstrapperStart
    {
        public static void Start()
        {
            var plugins = SmObjectFactory.Container.GetAllInstances<IPlugin>().ToList();
            using (var uow = SmObjectFactory.Container.GetInstance<IUnitOfWork>())
            {
                initDatabase(uow, plugins);
                runDatabaseSeeders(uow, plugins);
            }
        }

        private static void initDatabase(IUnitOfWork uow, IEnumerable<IPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                var efBootstrapper = plugin.GetEfBootstrapper();
                if (efBootstrapper == null) continue;

                uow.SetDynamicEntities(efBootstrapper.DomainEntities);
                uow.SetConfigurationsAssemblies(efBootstrapper.ConfigurationsAssemblies);
            }

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MvcPluginMasterAppContext, Configuration>());
            uow.ForceDatabaseInitialize();
        }

        private static void runDatabaseSeeders(IUnitOfWork uow, IEnumerable<IPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                var efBootstrapper = plugin.GetEfBootstrapper();
                if (efBootstrapper == null || efBootstrapper.DatabaseSeeder == null) continue;

                efBootstrapper.DatabaseSeeder(uow);
                uow.SaveAllChanges();
            }
        }
    }
}