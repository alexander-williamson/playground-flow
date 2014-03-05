namespace Flow.Library.Steps
{
    // collects information about a completed step
    // when steps have been completed, their id and version # must be known
    // so when flows and steps are restored, the steps are not re-run
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