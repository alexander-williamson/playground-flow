namespace Flow.Library.Steps
{
    public class StopStepBase : StepBase
    {
        public StopStepBase()
        {
            Id = int.MaxValue;
            Name = "StopStepBase";
            IsInitialized = true;
            CanContinue = true;
        }
    }
}