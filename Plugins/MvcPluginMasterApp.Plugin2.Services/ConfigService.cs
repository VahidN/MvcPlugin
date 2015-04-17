using MvcPluginMasterApp.Plugin2.Services.Contracts;

namespace MvcPluginMasterApp.Plugin2.Services
{
    public class ConfigService : IConfigService
    {
        public string Key2
        {
            get { return "Value 2"; }
        }
    }
}