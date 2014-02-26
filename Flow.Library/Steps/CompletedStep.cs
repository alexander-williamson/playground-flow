namespace Flow.Library.Steps
{
    public class CompletedStep
    {
        private readonly int _stepId;
        private readonly int _stepVersion;
        
        public int StepId { get { return _stepId; } }
        public int StepVersion { get { return _stepVersion; } } 
        
        public CompletedStep(int stepId, int stepVersion)
        {
            _stepId = stepId;
            _stepVersion = stepVersion;
        }

    }
}