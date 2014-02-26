using System.Collections.Generic;

namespace Flow.Library.Core
{
    public interface IDataManipulation
    {
        IDictionary<string, object> Manipulate(IDictionary<string, object> variables, string key);
    }
}
