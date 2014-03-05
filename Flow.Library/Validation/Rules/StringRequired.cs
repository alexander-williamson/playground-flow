using System.Collections.Generic;

namespace Flow.Library.Validation.Rules
{
    public class StringRequired : IValidationRule
    {
        public string VariableKey { get; set; }
        public bool Validate(IDictionary<string, object> variables, string key)
        {
            if (variables == null || !variables.ContainsKey(key))
                return false;

            return !string.IsNullOrWhiteSpace(variables[key].ToString());
        }
    }
}