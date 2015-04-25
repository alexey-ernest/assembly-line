using System.Configuration;

namespace AssemblyLine.Common.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string Get(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}