using System.Collections.Generic;
using Flow.Library.Core;
using Flow.Library.UI;
using Flow.Library.Validation;

namespace Flow.Library.Steps
{
    public class DataCollectionStep : StepBase
    {
        // the list of variables that this form needs
        // (this is so you can hide form only variables)
        //public Dictionary<string, object> AvailableVariables { get; private set; }

        // Variables after the 
        public Dictionary<string, object> Variables { get; set; }
        public IFormTemplate FormTemplate { get; set; }

        private bool _isValid;

        public DataCollectionStep(List<IValidationRule> rules = null) //, Dictionary<string, object> requiredVariables = null)
        {
            ExitRules = rules ?? new List<IValidationRule>(); 
        }

        public override void Process(FlowInstance flow, Runners.IRunFlows runner)
        {
            var engine = new ValidationEngine(ExitRules, Variables);
            _isValid = engine.IsValid;
            IsProcessed = true;
        }

        public override bool CanContinue { get { return _isValid; } }
    }
}