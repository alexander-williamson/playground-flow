using Flow.Library.Configuration;

namespace Flow.Web.Configuration
{
    public class WebConfiguration : IConfiguration
    {
        private readonly IConfigurationProvider _configurationProvider;

        public WebConfiguration(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        private static bool? _collectCastleWindsorPerformanceCounters;
        public bool CollectCastleWindsorPerformanceCounters {
            get
            {
                if (_collectCastleWindsorPerformanceCounters.HasValue == false)
                {
                    var configurationValue =
                        _configurationProvider.AppSettings["CollectCastleWindsorPerformanceCounters"];
                    var parsedValue = bool.Parse(configurationValue);
                    _collectCastleWindsorPerformanceCounters = parsedValue;
                }
                return _collectCastleWindsorPerformanceCounters.Value;
            }
        }

        private static string _connectionString;
        public string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    _connectionString = _configurationProvider.ConnectionString("Flow");
                }
                return _connectionString;
            }
        }
    }
}
