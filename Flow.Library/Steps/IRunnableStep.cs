using System.Collections.Generic;
using Flow.Library.Core;
using Flow.Library.Runners;
using Flow.Library.Validation;

namespace Flow.Library.Steps
{
    public interface IRunnableStep : IStep
    {
        bool IsComplete { get; }
        bool IsProcessed { get; set; }
        bool CanContinue { get; }
        bool CanInitialise { get; set; }
        bool IsInitialized { get; set; }
        void Initialise();

        // Entry rules (rules that must be fulfilled before we can run this rule)
        IList<IValidationRule> BrokenEntryRules(IDictionary<string, object> variables);

        // exit rules that must be fulfilled before this step can be marked as complete
        IList<IValidationRule> BrokenExitRules(IDictionary<string, object> variables);

        void Process(FlowInstance flow, IRunFlows runner);
    }
}