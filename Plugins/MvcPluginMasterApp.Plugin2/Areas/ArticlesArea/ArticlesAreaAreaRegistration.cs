using System.Web.Mvc;

namespace MvcPluginMasterApp.Plugin2.Areas.ArticlesArea
{
    public class ArticlesAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ArticlesArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ArticlesArea_default",
                "ArticlesArea/{controller}/{action}/{id}",
                // تكميل نام كنترلر پيش فرض
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                // مشخص كردن فضاي نام مرتبط جهت جلوگيري از تداخل با ساير قسمت‌هاي برنامه
                namespaces: new[] { string.Format("{0}.Controllers", this.GetType().Namespace) }

            );
        }
    }
}
