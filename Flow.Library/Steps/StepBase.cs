using Flow.Library.Validation;
using System.Collections.Generic;
using Flow.Library.Core;
using Flow.Library.Runners;

namespace Flow.Library.Steps
{
    public class StepBase : IStep
    {
        // steps are versioned so you can work out where you've resumed from
        public int Id { get; set; }
        public int VersionId { get; set; }

        public string Name { get; set; }

        // connections
        public int MinimumNodes { get; set; }
        public int MaximumNodes { get; set; }

        public bool IsComplete { get { return IsInitialized && IsProcessed && CanContinue; } }
        public bool IsProcessed { get; set; }

        // true if this step has completed completely
        public virtual bool CanContinue
        {
            get
            {
                if(!CanInitialise)
                    return false;

                if (!IsInitialized)
                    return false;

                if (!IsProcessed)
                    return false;

                if (EntryRules.Count > 0)
                    return false;

                if (ExitRules.Count > 0)
                    return false;

                return true;                    
            }
        } 

        // true if this step has everything it needs to initialise
        public bool CanInitialise { get; set; }

        // true if this step has initialised
        public bool IsInitialized { get; set; }

        // perform initialisation
        // this is an example
        public void Initialise() { IsInitialized = true; }

        // Entry rules (rules that must be fulfilled before we can run this rule)
        public List<IValidationRule> EntryRules { get; set; }
        public List<IValidationRule> BrokenEntryRules(IDictionary<string, object> variables)
        {
            if (EntryRules != null && variables != null)
            {
                var engine = new ValidationEngine(EntryRules, variables);
                engine.Validate();
                return engine.BrokenRules;
            }
            return new List<IValidationRule>();
        }

        // actions that can be performed during this step
        // this will only run after the entry rules have been validated
        public List<IDataManipulation> DataManipulations { get; private set; }

        // exit rules that must be fulfilled before this step can be marked as complete
        public List<IValidationRule> ExitRules { get; set; }
        public List<IValidationRule> BrokenExitRules(IDictionary<string, object> variables)
        {
            if (EntryRules != null && variables != null)
            {
                var engine = new ValidationEngine(EntryRules, variables);
                engine.Validate();
                return engine.BrokenRules;
            }
            return new List<IValidationRule>();
        }

        public virtual void Process(FlowInstance flow, IRunFlows runner)
        {
            IsProcessed = true;
        }

        public StepBase()
        {
            CanInitialise = true;
            EntryRules = new List<IValidationRule>();
            DataManipulations = new List<IDataManipulation>();
            ExitRules = new List<IValidationRule>();
        }
    }
}