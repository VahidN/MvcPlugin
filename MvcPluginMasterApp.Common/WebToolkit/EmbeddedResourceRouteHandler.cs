using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace MvcPluginMasterApp.Common.WebToolkit
{
    public class EmbeddedResourceRouteHandler : IRouteHandler
    {
        private readonly Assembly _assembly;
        private readonly string _resourcePath;
        private readonly TimeSpan _cacheDuration;

        public EmbeddedResourceRouteHandler(Assembly assembly, string resourcePath, TimeSpan cacheDuration)
        {
            _assembly = assembly;
            _resourcePath = resourcePath;
            _cacheDuration = cacheDuration;
        }

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new EmbeddedResourceHttpHandler(requestContext.RouteData, _assembly, _resourcePath, _cacheDuration);
        }
    }

    public class EmbeddedResourceHttpHandler : IHttpHandler
    {
        private readonly RouteData _routeData;
        private readonly Assembly _assembly;
        private readonly string _resourcePath;
        private readonly TimeSpan _cacheDuration;

        public EmbeddedResourceHttpHandler(
            RouteData routeData, Assembly assembly, string resourcePath, TimeSpan cacheDuration)
        {
            _routeData = routeData;
            _assembly = assembly;
            _resourcePath = resourcePath;
            _cacheDuration = cacheDuration;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var routeDataValues = _routeData.Values;
            var fileName = routeDataValues["file"].ToString();
            var fileExtension = routeDataValues["extension"].ToString();

            var manifestResourceName = string.Format("{0}.{1}.{2}", _resourcePath, fileName, fileExtension);
            var stream = _assembly.GetManifestResourceStream(manifestResourceName);
            if (stream == null)
            {
                throw new KeyNotFoundException(string.Format("Embedded resource key: '{0}' not found in the '{1}' assembly.", manifestResourceName, _assembly.FullName));
            }

            context.Response.Clear();
            context.Response.ContentType = "application/octet-stream";
            cacheIt(context.Response, _cacheDuration);
            stream.CopyTo(context.Response.OutputStream);
        }

        private static void cacheIt(HttpResponse response, TimeSpan duration)
        {
            var cache = response.Cache;

            var maxAgeField = cache.GetType().GetField("_maxAge", BindingFlags.Instance | BindingFlags.NonPublic);
            if (maxAgeField != null) maxAgeField.SetValue(cache, duration);

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetExpires(DateTime.Now.Add(duration));
            cache.SetMaxAge(duration);
            cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
        }
    }
}