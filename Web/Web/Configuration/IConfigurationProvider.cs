using System.Collections.Specialized;

namespace Flow.Web.Configuration
{
    public interface IConfigurationProvider
    {
        NameValueCollection AppSettings { get; }
        string ConnectionString(string value);
    }
}