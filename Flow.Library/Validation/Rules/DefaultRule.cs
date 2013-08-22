using System.Collections.Generic;

namespace Flow.Library.Validation.Rules
{
    public class DefaultRule : IRule
    {
        public bool Validate(IDictionary<string, object> variables, string key)
        {
            return false;
        }
    }
}
