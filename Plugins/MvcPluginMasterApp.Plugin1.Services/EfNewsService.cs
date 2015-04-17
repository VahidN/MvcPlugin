using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MvcPluginMasterApp.Plugin1.DomainClasses;
using MvcPluginMasterApp.Plugin1.Services.Contracts;
using MvcPluginMasterApp.PluginsBase;

namespace MvcPluginMasterApp.Plugin1.Services
{
    public class EfNewsService:INewsService
    {
        IUnitOfWork _uow;
        readonly IDbSet<News> _news;
        public EfNewsService(IUnitOfWork uow)
        {
            _uow = uow;
            _news = _uow.Set<News>();
        }

        public void AddNews(News news)
        {
            _news.Add(news);
        }

        public IList<News> GetAllNews()
        {
            return _news.ToList();
        }
    }
}