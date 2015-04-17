using System.Collections.Generic;
using MvcPluginMasterApp.Plugin2.DomainClasses;

namespace MvcPluginMasterApp.Plugin2.Services.Contracts
{
    public interface IArticlesService
    {
        void AddArticle(Article news);
        IList<Article> GetAllArticles();
    }
}