using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowInstanceRepository
    {
        FlowInstance Get(int id);
        int Add(FlowInstance instance);
    }
}