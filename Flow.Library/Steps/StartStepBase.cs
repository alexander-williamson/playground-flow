namespace Flow.Library.Steps
{
    public class StartStepBase : StepBase
    {
        public StartStepBase(FlowInstance flow) : base(flow)
        {
            Id = int.MinValue;
            Name = "Start";
            IsInitialized = true;
            IsProcessed = true;
            CanContinue = true;
        }
    }
}