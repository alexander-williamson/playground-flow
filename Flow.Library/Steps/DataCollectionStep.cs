using System.Collections.Generic;
using System.Dynamic;
using Flow.Library.Core;
using Flow.Library.Validation;

namespace Flow.Library.Steps
{
    public class DataCollectionStep : StepBase
    {
        public List<ValidationRule> Rules { get; set; }
        private Dictionary<string, object> Variables { get; set; }

        public DataCollectionStep()
        {
            Rules = new List<ValidationRule>();
            Variables = new Dictionary<string, object>();
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

        public DataCollectionStep(List<ValidationRule> rules = null)
        {
            Rules = rules ?? new List<ValidationRule>();
        }
    }
}