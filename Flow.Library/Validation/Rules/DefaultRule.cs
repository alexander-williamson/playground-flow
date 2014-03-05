using System.Collections.Generic;

namespace Flow.Library.Validation.Rules
{
    public class DefaultRule : IValidationRule
    {
        public string VariableKey { get; set; }
        public bool Validate(IDictionary<string, object> variables, string key)
        {
            return false;
        }
    }
}
