using Flow.Library.Core;
using Flow.Library.Runners;
using Flow.Library.UI;

namespace Flow.Library.Steps
{
    public class StepBase
    {
        // steps are versioned so you can work out where you've resumed from
        public int Id { get; set; }
        public int VersionId { get; set; }

        public string Type { get; set; }
        public string Name { get; set; }
        public int MinimumNodes { get; set; }
        public int MaximumNodes { get; set; }
        public Coordinate Coordinate { get; set; }
        public bool IsComplete { get { return IsInitialized && IsProcessed && CanContinue; } }
        public bool IsInitialized { get; set; }
        public bool IsProcessed { get; set; }
        public virtual bool CanContinue { get; set; }
        public bool CanInitialise { get; set; }
        public void Initialise() { IsInitialized = true; }

        public virtual void Process(FlowInstance flow, IRunFlows runner)
        {
            IsProcessed = true;
            CanContinue = true;
        }

        public StepBase()
        {
            CanInitialise = true;
        }

        public override string ToString()
        {
            return "StepBase";
        }
    }
}