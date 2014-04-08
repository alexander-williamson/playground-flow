using System.Configuration;
using Flow.Library.Configuration;

namespace Flow.Web.Configuration
{
    public class WebConfiguration : IConfiguration
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Flow"].ConnectionString;
            }
        }
    }
}
