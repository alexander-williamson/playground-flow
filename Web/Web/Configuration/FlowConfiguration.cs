using Flow.Library.Configuration;

namespace Flow.Web.Configuration
{
    public class WebConfiguration : IConfiguration
    {
        private readonly IConfigurationProvider _provider;

        public WebConfiguration(IConfigurationProvider provider)
        {
            _provider = provider;
        }

        public bool CollectCastleWindsorPerformanceCounters {
            get
            {
                var configurationValue = _provider.AppSettings["CollectCastleWindsorPerformanceCounters"];
                var parsedValue = bool.Parse(configurationValue);
                return parsedValue;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _provider.ConnectionString("Flow");
            }
        }
    }
}
