using System.Collections;
using System.Collections.Generic;
using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowTemplateRepository
    {
        IEnumerable<FlowTemplate> Get();
        FlowTemplate Get(int id);
        int Add(FlowTemplate instance);
        bool Update(int id, FlowTemplate instance);
        bool Delete(int id);
    }
}