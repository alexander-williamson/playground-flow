using Flow.Library.Steps;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowTemplateStep : IStep
    {
        int FlowTemplateId { get; set; }
    }
}