using Flow.Library.Runners;

namespace Flow.Library.Steps
{
    public class StepBase
    {
        protected FlowInstance Flow { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinimumNodes { get; set; }
        public int MaximumNodes { get; set; }
        public Coordinate Coordinate { get; set; }
        public bool IsComplete { get { return IsInitialized && IsProcessed && CanContinue; } }
        public bool IsInitialized { get; set; }
        public bool IsProcessed { get; set; }
        public bool CanContinue { get; protected set; }
        public bool CanInitialise { get; set; }
        public void Initialise()
        {
            IsInitialized = true;
        }
        public virtual void Process(FlowInstance flow, IRunFlows runner)
        {
            IsProcessed = true;
            CanContinue = true;
        }
        public StepBase(FlowInstance flow)
        {
            Flow = flow;
            CanInitialise = true;
        }
        public override string ToString()
        {
            return "StepBase";
        }
    }
}