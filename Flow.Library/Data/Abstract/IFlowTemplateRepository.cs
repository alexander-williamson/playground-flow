using System.Collections.Generic;
using System.Data;
using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowTemplateRepository
    {
        IEnumerable<FlowTemplate> Get(IDbTransaction transaction = null);
        FlowTemplate Get(int id, IDbTransaction transaction = null);
        int Add(FlowTemplate instance, IDbTransaction transaction = null);
        void Update(int id, FlowTemplate instance, IDbTransaction transaction = null);
        void Delete(int id, IDbTransaction transaction = null);
    }
}