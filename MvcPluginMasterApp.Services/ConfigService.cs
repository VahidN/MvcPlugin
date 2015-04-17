using MvcPluginMasterApp.Services.Contracts;

namespace MvcPluginMasterApp.Services
{
    public class ConfigService : IConfigService
    {
        public string MasterKey
        {
            get { return "Master Key 1"; }
        }
    }
}