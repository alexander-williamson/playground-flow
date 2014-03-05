using System.Collections.Generic;

namespace Flow.Library.Validation.Rules
{
    public class StringMaxLength : IValidationRule
    {
        public string VariableKey { get; set; }
        public int MaxLength { get; set; }
        public bool Validate(IDictionary<string, object> variables, string key)
        {
            if (!variables.ContainsKey(key))
                return false;

            var o = variables[key].ToString();
            
            return o.Length <= MaxLength;
        }
    }
}