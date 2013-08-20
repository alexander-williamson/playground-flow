using Flow.Library.Steps;

namespace Flow.Library.Actions
{
    public interface IAction
    {
        StepBase Step { get; set; }
    }
}