using Flow.Library.Data.Models;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowInstanceRepository
    {
        FlowInstanceDataModel GetFlow(int id);
    }
}