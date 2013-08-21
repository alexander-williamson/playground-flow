using System.Collections.Generic;

namespace Flow.Library.Validation.Rules
{
    public class DefaultRule : IValidate
    {
        public bool Validate(IDictionary<string, object> variables, string key)
        {
            return false;
        }
    }
}
