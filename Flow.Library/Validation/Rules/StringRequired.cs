using System.Collections.Generic;

namespace Flow.Library.Validation.Rules
{
    public class StringRequired : IRule
    {
        public bool Validate(IDictionary<string, object> variables, string key)
        {
            if (!variables.ContainsKey(key))
                return false;

            return !string.IsNullOrWhiteSpace(variables[key].ToString());
        }
    }
}