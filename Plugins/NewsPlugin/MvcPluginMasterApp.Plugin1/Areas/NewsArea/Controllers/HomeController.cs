using System.Web.Mvc;
using MvcPluginMasterApp.Plugin1.Services.Contracts;

namespace MvcPluginMasterApp.Plugin1.Areas.NewsArea.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigService _configService;
        private readonly INewsService _newsService;

        public HomeController(IConfigService configService, INewsService newsService)
        {
            _configService = configService;
            _newsService = newsService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "From Plugin1: " + _configService.Key1;
            var newsList = _newsService.GetAllNews();
            return View(model: newsList);
        }
    }
}