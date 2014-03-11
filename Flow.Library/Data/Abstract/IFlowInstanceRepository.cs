using System.Data;
using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowInstanceRepository
    {
        FlowInstance Get(int id, IDbTransaction transaction = null);
        int Add(FlowInstance instance, IDbTransaction transaction = null);
    }
}