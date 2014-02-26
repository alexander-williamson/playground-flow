using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowInstanceRepository
    {
        FlowInstance GetFlow(int id);
    }
}