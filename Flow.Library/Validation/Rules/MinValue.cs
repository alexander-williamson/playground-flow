using System.Collections.Generic;

namespace Flow.Library.Validation.Rules
{
    public class MinValue : IValidationRule
    {
        public string VariableKey { get; set; }
        public int Min { get; set; }
        public bool Validate(IDictionary<string, object> variables, string key)
        {
            if (!variables.ContainsKey(key))
                return false;

            int o;

            if (!int.TryParse(variables[key].ToString(), out o))
                return false;

            return Min <= (int)o;
        }

    }
}