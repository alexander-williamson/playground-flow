using System.Collections.Generic;
using Flow.Library;

namespace Flow.Library.Validation.Rules
{
    public class StringMaxLength : IValidate
    {
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