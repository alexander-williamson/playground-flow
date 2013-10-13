namespace Flow.Library.Steps
{
    public class StartStepBase : StepBase
    {
        public StartStepBase()
        {
            Id = int.MinValue;
            Name = "Start";
            IsInitialized = true;
            IsProcessed = true;
        }
    }
}