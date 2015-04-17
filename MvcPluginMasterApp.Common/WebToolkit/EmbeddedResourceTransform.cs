using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Optimization;

namespace MvcPluginMasterApp.Common.WebToolkit
{
    public class EmbeddedResourceTransform : IBundleTransform
    {
        private readonly IList<string> _resourceFiles;
        private readonly string _contentType;
        private readonly Assembly _assembly;

        public EmbeddedResourceTransform(IList<string> resourceFiles, string contentType, Assembly assembly)
        {
            _resourceFiles = resourceFiles;
            _contentType = contentType;
            _assembly = assembly;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var result = new StringBuilder();

            foreach (var resource in _resourceFiles)
            {
                using (var stream = _assembly.GetManifestResourceStream(resource))
                {
                    if (stream == null)
                    {
                        throw new KeyNotFoundException(string.Format("Embedded resource key: '{0}' not found in the '{1}' assembly.", resource, _assembly.FullName));
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        result.Append(reader.ReadToEnd());
                    }
                }
            }

            response.ContentType = _contentType;
            response.Content = result.ToString();
        }
    }
}