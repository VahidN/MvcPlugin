using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MvcPluginMasterApp.Plugin2.DomainClasses;
using MvcPluginMasterApp.Plugin2.Services.Contracts;
using MvcPluginMasterApp.PluginsBase;

namespace MvcPluginMasterApp.Plugin2.Services
{
    public class EfArticlesService : IArticlesService
    {
        IUnitOfWork _uow;
        readonly IDbSet<Article> _articles;
        public EfArticlesService(IUnitOfWork uow)
        {
            _uow = uow;
            _articles = _uow.Set<Article>();
        }

        public void AddArticle(Article news)
        {
            _articles.Add(news);
        }

        public IList<Article> GetAllArticles()
        {
            return _articles.ToList();
        }
    }
}