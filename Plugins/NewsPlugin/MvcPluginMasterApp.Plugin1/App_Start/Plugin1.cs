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
using MvcPluginMasterApp.Plugin1.DomainClasses;
using MvcPluginMasterApp.Plugin1.Services;
using MvcPluginMasterApp.Plugin1.Services.Contracts;
using MvcPluginMasterApp.PluginsBase;

namespace MvcPluginMasterApp.Plugin1
{
    public class Plugin1 : IPlugin
    {
        public EfBootstrapper GetEfBootstrapper()
        {
            return new EfBootstrapper
            {
                DomainEntities = new[] { typeof(News), typeof(User) },
                ConfigurationsAssemblies = new[]
                {
                    typeof(NewsConfig).Assembly,
                    typeof(User).Assembly
                },
                DatabaseSeeder = uow =>
                {
                    var news = uow.Set<News>();
                    if (news.Any())
                    {
                        return;
                    }

                    var users = uow.Set<User>();
                    var user1 = users.Add(new User { Name = "User Plugin 1" });

                    news.Add(new News
                    {
                        Title = "News 1",
                        Body = "news 1 news 1 news 1 ....",
                        User = user1
                    });

                    news.Add(new News
                    {
                        Title = "News 2",
                        Body = "news 2 news 2 news 2 ....",
                        User = user1
                    });
                }
            };
        }

        public MenuItem GetMenuItem(RequestContext requestContext)
        {
            return new MenuItem
            {
                Name = "Plugin 1",
                Url = new UrlHelper(requestContext).Action("Index", "Home", new { area = "NewsArea" })
            };
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            // Mostly the default namespace and assembly name are the same
            var assemblyNameSpace = executingAssembly.GetName().Name;

            var scriptsBundle = new Bundle("~/Plugin1/Scripts",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace + ".Scripts.test1.js"
                }, "application/javascript", executingAssembly));

            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                scriptsBundle.Transforms.Add(new JsMinify());
            }

            bundles.Add(scriptsBundle);

            var cssBundle = new Bundle("~/Plugin1/Content",
                new EmbeddedResourceTransform(new List<string>
                {
                    assemblyNameSpace + ".Content.test1.css"
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
                new Route("NewsArea/Images/{file}.{extension}",
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
                cfg.For<INewsService>().Use<EfNewsService>();
            });
        }
    }
}