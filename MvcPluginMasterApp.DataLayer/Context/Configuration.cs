using System.Data.Entity.Migrations;
using System.Linq;
using MvcPluginMasterApp.DomainClasses;

namespace MvcPluginMasterApp.DataLayer.Context
{
    public class Configuration : DbMigrationsConfiguration<MvcPluginMasterAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MvcPluginMasterAppContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var cat1 = context.Categories.Add(new Category
            {
                Name = "Cat 1",
                Title = "T1"
            });
            var cat2 = context.Categories.Add(new Category
            {
                Name = "Cat 2",
                Title = "T2"
            });

            context.Products.Add(new Product
            {
                Category = cat1,
                Name = "P1",
                Price = 1000
            });

            context.Products.Add(new Product
            {
                Category = cat2,
                Name = "P2",
                Price = 2000
            });


            base.Seed(context);
        }
    }
}
