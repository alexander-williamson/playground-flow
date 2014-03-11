using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowTemplateRepository
    {
        FlowTemplate GetTemplate(int id);
    }
}