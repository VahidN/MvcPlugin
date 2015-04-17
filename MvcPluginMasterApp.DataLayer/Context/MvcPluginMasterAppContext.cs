using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MvcPluginMasterApp.DomainClasses;
using MvcPluginMasterApp.PluginsBase;
using System;
using System.Reflection;

namespace MvcPluginMasterApp.DataLayer.Context
{
    public class MvcPluginMasterAppContext : DbContext, IUnitOfWork
    {
        private readonly IList<Assembly> _configurationsAssemblies = new List<Assembly>();
        private readonly IList<Type[]> _dynamicTypes = new List<Type[]>();

        /// <summary>
        /// It looks for a connection string named Sample07Context in the web.config file.
        /// </summary>
        public MvcPluginMasterAppContext()
            : base("connectionString1")
        {
        }

        public DbSet<Category> Categories { set; get; }
        public DbSet<Product> Products { set; get; }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void ForceDatabaseInitialize()
        {
            Database.Initialize(force: true);
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void SetConfigurationsAssemblies(Assembly[] assemblies)
        {
            if (assemblies == null) return;

            foreach (var assembly in assemblies)
            {
                _configurationsAssemblies.Add(assembly);
            }
        }

        public void SetDynamicEntities(Type[] dynamicTypes)
        {
            if (dynamicTypes == null) return;
            _dynamicTypes.Add(dynamicTypes);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            addConfigurationsFromAssemblies(modelBuilder);
            addPluginsEntitiesDynamically(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void addConfigurationsFromAssemblies(DbModelBuilder modelBuilder)
        {
            foreach (var assembly in _configurationsAssemblies)
            {
                modelBuilder.Configurations.AddFromAssembly(assembly);
            }
        }

        private void addPluginsEntitiesDynamically(DbModelBuilder modelBuilder)
        {
            foreach (var types in _dynamicTypes)
            {
                foreach (var type in types)
                {
                    modelBuilder.RegisterEntityType(type);
                }
            }
        }
    }
}