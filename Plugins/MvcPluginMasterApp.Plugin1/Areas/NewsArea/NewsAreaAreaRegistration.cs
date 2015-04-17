using System.Web.Mvc;

namespace MvcPluginMasterApp.Plugin1.Areas.NewsArea
{
    public class NewsAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "NewsArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "NewsArea_default",
                "NewsArea/{controller}/{action}/{id}",
                // تكميل نام كنترلر پيش فرض
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                // مشخص كردن فضاي نام مرتبط جهت جلوگيري از تداخل با ساير قسمت‌هاي برنامه
                namespaces: new[] { string.Format("{0}.Controllers", this.GetType().Namespace) }
            );
        }
    }
}
