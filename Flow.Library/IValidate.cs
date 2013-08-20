using System.Collections.Generic;

namespace Flow.Library
{
    public interface IValidate
    {
        bool Validate(IDictionary<string, object> variables, string key);
    }
}