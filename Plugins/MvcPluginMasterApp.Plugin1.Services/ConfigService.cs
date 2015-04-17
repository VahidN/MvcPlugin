using MvcPluginMasterApp.Plugin1.Services.Contracts;

namespace MvcPluginMasterApp.Plugin1.Services
{
    public class ConfigService : IConfigService
    {
        public string Key1
        {
            get { return "Value 1"; }
        }
    }
}