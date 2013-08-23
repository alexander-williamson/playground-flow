using System.Collections.Generic;
using System.Linq;
using Flow.Library.Core;
using Flow.Library.UI;
using Flow.Library.Validation;

namespace Flow.Library.Steps
{
    public class DataCollectionStep : StepBase
    {
        // Rules can match anything in the whole flow
        public List<ValidationRule> Rules { get; set; }
        public List<ValidationRule> BrokenRules
        {
            get
            {
                if (Rules != null && Variables != null)
                {
                    var engine = new ValidationEngine(Rules, Variables);
                    engine.Validate();
                    return engine.BrokenRules;
                }
                return new List<ValidationRule>();
            }
        } 

        // the list of variables that this form needs
        // (this is so you can hide form only variables)
        public Dictionary<string, object> AvailableVariables { get; private set; }

        // Variables after the 
        public Dictionary<string, object> Variables { get; set; }
        public IFormTemplate FormTemplate { get; set; }

        public DataCollectionStep(List<ValidationRule> rules = null, Dictionary<string, object> requiredVariables = null)
        {
            Rules = rules ?? new List<ValidationRule>();
            AvailableVariables = requiredVariables ?? new Dictionary<string, object>();
            Variables = AvailableVariables;
        }

        public override void Process(FlowInstance flow, Runners.IRunFlows runner)
        {
            Variables = flow.Variables;
            if (CanContinue)
            {
                IsProcessed = true;
            }
        }

        public override bool CanContinue 
        { 
            get
            {
                var engine = new ValidationEngine(Rules, Variables);
                return engine.IsValid;
            } 
        }
    }
}