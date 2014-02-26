namespace Flow.Library.Steps
{
    public class StartStep : StepBase
    {
        public StartStep()
        {
            Id = int.MinValue;
            Name = "Start";
            IsInitialized = true;
            IsProcessed = true;
        }
    }
}