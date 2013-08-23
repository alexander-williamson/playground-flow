using System.Collections.Generic;
using System.Linq;

namespace Flow.Library.Validation
{
    public class ValidationEngine
    {
        public List<ValidationRule> Rules { get; private set; }
        public IDictionary<string, object> Variables { get; set; } 

        private readonly List<ValidationRule> _brokenRules;
        public List<ValidationRule> BrokenRules { get { return _brokenRules; } } 

        public ValidationEngine(List<ValidationRule> rules, IDictionary<string, object> variables)
        {
            Rules = rules;
            Variables = variables;
            _brokenRules = new List<ValidationRule>();
        }

        public ValidationEngine(List<ValidationRule> rules, object objectToValidate)
        {
            Variables = new Dictionary<string, object>();
            foreach(var property in objectToValidate.GetType().GetProperties())
            {
                var key = property.Name;
                var value = objectToValidate.GetType().GetProperty(key).GetValue(objectToValidate, null);
                Variables.Add(key, value);
            }

            Rules = rules;
            _brokenRules = new List<ValidationRule>();
        }

        public void Validate()
        {
            _brokenRules.Clear();

            // a list of variables that have rules defined for them
            var runRulesOn = (from o in Rules select o.Key).Distinct().ToList();

            // run the rules for each project
            foreach (var propertyName in runRulesOn)
            {
                var key = propertyName;
                var rulesForThisProperty = (from o in Rules where o.Key == key select o);
                foreach (var rule in rulesForThisProperty)
                {
                    if (!rule.Validator.Validate(Variables, propertyName))
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
