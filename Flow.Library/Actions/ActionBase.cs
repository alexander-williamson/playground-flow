using Flow.Library.Steps;

namespace Flow.Library.Actions
{
    public abstract class ActionBase
    {
        public IStep Step { get; set; }
    }
}