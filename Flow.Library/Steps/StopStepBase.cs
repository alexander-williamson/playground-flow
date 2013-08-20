namespace Flow.Library.Steps
{
    public class StopStepBase : StepBase
    {
        public StopStepBase(FlowInstance flow) : base(flow)
        {
            Id = int.MaxValue;
            Name = "StopStepBase";
            IsInitialized = true;
            CanContinue = true;
        }
    }
}