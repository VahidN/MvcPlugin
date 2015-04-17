using System.Collections.Generic;
using MvcPluginMasterApp.Plugin1.DomainClasses;

namespace MvcPluginMasterApp.Plugin1.Services.Contracts
{
    public interface INewsService
    {
        void AddNews(News news);
        IList<News> GetAllNews();
    }
}