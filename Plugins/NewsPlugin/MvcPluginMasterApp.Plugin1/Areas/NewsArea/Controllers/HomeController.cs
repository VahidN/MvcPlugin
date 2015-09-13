using System.Web.Mvc;
using MvcPluginMasterApp.Plugin1.DomainClasses;
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
            ViewBag.Message = string.Format("From Plugin1: {0}", _configService.Key1);
            var newsList = _newsService.GetAllNews();
            return View(model: newsList);
        }

        public ActionResult ShowResources()
        {
            /*
              <system.web>
                <globalization culture="fa-IR" uiCulture="fa-IR" />
             */
            var model = new News {Title = "Title 1", Body = "Body 1"};
            return View(model);
        }
    }
}