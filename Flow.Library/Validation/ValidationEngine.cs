using System.Collections.Generic;
using System.Linq;

namespace Flow.Library.Validation
{
    public class ValidationEngine
    {
        public List<IValidationRule> Rules { get; private set; }
        public IDictionary<string, object> Variables { get; set; } 

        private readonly List<IValidationRule> _brokenRules;
        public List<IValidationRule> BrokenRules { get { return _brokenRules; } }

        public ValidationEngine(IEnumerable<IValidationRule> rules, IDictionary<string, object> variables)
        {
            Rules = rules.ToList();
            Variables = variables;
            _brokenRules = new List<IValidationRule>();
        }

        public ValidationEngine(IEnumerable<IValidationRule> rules, object objectToValidate)
        {
            Variables = new Dictionary<string, object>();
            foreach(var property in objectToValidate.GetType().GetProperties())
            {
                var key = property.Name;
                var value = objectToValidate.GetType().GetProperty(key).GetValue(objectToValidate, null);
                Variables.Add(key, value);
            }

            Rules = rules.ToList();
            _brokenRules = new List<IValidationRule>();
        }

        public void Validate()
        {
            _brokenRules.Clear();

            // a list of variables that have rules defined for them
            var runRulesOn = (from o in Rules select o.VariableKey).Distinct().ToList();

            // run the rules for each project
            foreach (var propertyName in runRulesOn)
            {
                var key = propertyName;
                var rulesForThisProperty = (from o in Rules where o.VariableKey == key select o);
                foreach (var rule in rulesForThisProperty)
                {
                    if (!rule.Validate(Variables, propertyName))
                    {
                        BrokenRules.Add(rule);
                    }
                }
            }
        }

        public bool IsValid
        {
            get
            {
                Validate();
                return BrokenRules.Count == 0;
            }
        }
    }
}
