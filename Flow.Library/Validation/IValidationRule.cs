using System.Collections.Generic;

namespace Flow.Library.Validation
{
    public interface IValidationRule
    {
        string VariableKey { get; set; }
        bool Validate(IDictionary<string, object> variables, string key);
    }
}