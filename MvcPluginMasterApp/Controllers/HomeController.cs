using MvcPluginMasterApp.Services.Contracts;
using System.Web.Mvc;

namespace MvcPluginMasterApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigService _configService;

        public HomeController(IConfigService configService)
        {
            _configService = configService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = string.Format("From MvcPluginMasterApp: {0}", _configService.MasterKey);
            return View();
        }
    }
}