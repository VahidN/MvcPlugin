using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CommonEntities;
using MvcPluginMasterApp.Common.WebToolkit;
using MvcPluginMasterApp.Plugin2.DomainClasses;
using MvcPluginMasterApp.Plugin2.Services;
using MvcPluginMasterApp.Plugin2.Services.Contracts;
using MvcPluginMasterApp.PluginsBase;

namespace MvcPluginMasterApp.Plugin2
{
    public class Plugin2 : IPlugin
    {
        public EfBootstrapper GetEfBootstrapper()
        {
            return new EfBootstrapper
            {
                DomainEntities = new[] { typeof(Article), typeof(User) },
                ConfigurationsAssemblies = new[]
                {
                    typeof(ArticlesConfig).Assembly,
                    typeof(User).Assembly
                },
                DatabaseSeeder = uow =>
                {
                    var news = uow.Set<Article>();
                    if (news.Any())
                    {
                        return;
                    }

                    var users = uow.Set<User>();
                    var user1 = users.Add(new User { Name = "User Plugin 2" });

                    news.Add(new Article
                    {
                        Title = "Article 1",
                        Body = "data 1 data 1 data 1 ....",
                        User = user1
                    });

                    news.Add(new Article
                    {
                        Title = "Article 2",
                        Body = "data 2 data 2 data 2 ....",
                        User = user1
                    });
                }
            };
        }

        public MenuItem GetMenuItem(RequestContext requestContext)
        {
            return new MenuItem
            {
                Name = "Plugin 2",
                Url = new UrlHelper(requestContext).Action("Index", "Home", new { area = "ArticlesArea" })
            };
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            // Mostly the default namespace and assembly name are the same
            var assemblyNameSpace = executingAssembly.GetName().Name;

            var scriptsBundle = new Bundle("~/Plugin2/Scripts",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace + ".Scripts.test2.js"
                }, "application/javascript", executingAssembly));

            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                scriptsBundle.Transforms.Add(new JsMinify());
            }

            bundles.Add(scriptsBundle);

            var cssBundle = new Bundle("~/Plugin2/Content",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace + ".Content.test2.css"
                }, "text/css", executingAssembly));

            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                cssBundle.Transforms.Add(new CssMinify());
            }

            bundles.Add(cssBundle);

            BundleTable.EnableOptimizations = true;
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            //todo: add custom routes.

            var assembly = Assembly.GetExecutingAssembly();
            // Mostly the default namespace and assembly name are the same
            var nameSpace = assembly.GetName().Name;
            var resourcePath = string.Format("{0}.Images", nameSpace);

            routes.Insert(0,
                new Route("ArticlesArea/Images/{file}.{extension}",
                    new RouteValueDictionary(new { }),
                    new RouteValueDictionary(new { extension = "png|jpg" }),
                    new EmbeddedResourceRouteHandler(assembly, resourcePath, cacheDuration: TimeSpan.FromDays(30))
                ));
        }

        public void RegisterServices(StructureMap.IContainer container)
        {
            // todo: add custom services.

            container.Configure(cfg =>
            {
                cfg.For<IArticlesService>().Use<EfArticlesService>();
            });
        }
    }
}