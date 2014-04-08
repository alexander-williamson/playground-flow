using System.Collections.Specialized;
using System.Configuration;

namespace Flow.Web.Configuration
{
    public class ConfigurationManagerProvider : IConfigurationProvider
    {
        public NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }
        public string ConnectionString(string value)
        {
            return ConfigurationManager.ConnectionStrings[value].ConnectionString;
        }
    }
}