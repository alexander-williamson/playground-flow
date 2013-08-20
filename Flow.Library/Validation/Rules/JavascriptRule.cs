using System;
using System.Collections.Generic;
using Noesis.Javascript;

namespace Flow.Library.Validation.Rules
{
    public class JavascriptRule : IValidate
    {
        public const string ArgumentVariable = "argument";
        public const string FlowVariable = "flow";
        public const string ResultVariable = "result";

        public string Source { get; set; }

        public bool Validate(IDictionary<string, object> variables, string key)
        {

            try
            {
                using (var context = new JavascriptContext())
                {
                    // set the argument
                    context.SetParameter(ArgumentVariable, variables[key]);

                    // set useful variables
                    foreach(var element in variables)
                        context.SetParameter(element.Key, element.Value.ToString());

                    // run the rule with the custom javascript
                    context.Run(Source);

                    // return the value if it can be found
                    // if garbled, return false
                    var result = context.GetParameter(ResultVariable);
                    return result != null && (bool)result;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
