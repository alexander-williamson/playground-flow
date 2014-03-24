using System.Collections.Generic;
using Flow.Library.Core;
using Flow.Library.Runners;
using Flow.Library.Validation;

namespace Flow.Library.Steps
{

    public interface IStep : ITrackDirty
    {
        int Id { get; set; }
        int VersionId { get; set; }
        string Name { get; set; }
        
        List<IValidationRule> EntryRules { get; set; }
        List<IValidationRule> ExitRules { get; set; }
    }

    public interface IRunnableStep : IStep
    {
        bool IsComplete { get; }
        bool IsProcessed { get; set; }
        bool CanContinue { get; }
        bool CanInitialise { get; set; }
        bool IsInitialized { get; set; }
        void Initialise();

        // Entry rules (rules that must be fulfilled before we can run this rule)
        List<IValidationRule> BrokenEntryRules(IDictionary<string, object> variables);

        // exit rules that must be fulfilled before this step can be marked as complete
        List<IValidationRule> BrokenExitRules(IDictionary<string, object> variables);

        void Process(FlowInstance flow, IRunFlows runner);
    }
}