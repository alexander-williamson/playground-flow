using System.Collections.Generic;

namespace Flow.Library.Validation
{
    public interface IRule
    {
        bool Validate(IDictionary<string, object> variables, string key);
    }
}