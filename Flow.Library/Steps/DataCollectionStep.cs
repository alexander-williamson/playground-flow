using System.Collections.Generic;
using System.Dynamic;
using Flow.Library.Validation;

namespace Flow.Library.Steps
{
    public class DataCollectionStep : StepBase
    {
        public List<ValidationRule> Rules { get; set; }

        public new bool CanContinue 
        { 
            get
            {
                var instance = new ExpandoObject();
                foreach (var element in Flow.Variables)
                    ((IDictionary<string, object>)instance).Add(element.Key, element.Value);

                var engine = new ValidationEngine(Rules, instance);
                return engine.IsValid;
            } 
        }

        public DataCollectionStep(FlowInstance flow, List<ValidationRule> rules = null) : base(flow)
        {
            Rules = rules ?? new List<ValidationRule>();
        }
    }
}