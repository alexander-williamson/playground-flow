using System.Collections.Generic;

namespace Flow.Library.Data
{
    public interface IDataContext
    {
        IEnumerable<T> Query<T>();
        IEnumerable<T> Query<T>(string query, object parameters);
    }
}
