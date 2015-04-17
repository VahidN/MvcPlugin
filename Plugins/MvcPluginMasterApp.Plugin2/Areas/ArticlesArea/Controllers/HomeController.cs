using System.Web.Mvc;
using MvcPluginMasterApp.Plugin2.Services.Contracts;

namespace MvcPluginMasterApp.Plugin2.Areas.ArticlesArea.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigService _configService;
        private readonly IArticlesService _articlesService;

        public HomeController(IConfigService configService, IArticlesService articlesService)
        {
            _configService = configService;
            _articlesService = articlesService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = string.Format("From Plugin2: {0}", _configService.Key2);
            var articlesList = _articlesService.GetAllArticles();
            return View(model: articlesList);
        }
    }
}