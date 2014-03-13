using System.Collections.Generic;
using System.Data;
using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowTemplateRepository
    {
        IEnumerable<Core.FlowTemplate> Get(IDbTransaction transaction = null);
        Core.FlowTemplate Get(int id, IDbTransaction transaction = null);
        int Add(Core.FlowTemplate instance, IDbTransaction transaction = null);
        void Update(int id, Core.FlowTemplate instance, IDbTransaction transaction = null);
        void Delete(int id, IDbTransaction transaction = null);
    }
}