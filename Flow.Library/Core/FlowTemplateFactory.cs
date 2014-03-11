using System.Data;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Core
{
    public class FlowTemplateFactory
    {
        private readonly IFlowTemplateRepository _flowTemplateRepository;
        public FlowTemplateFactory(IFlowTemplateRepository flowTemplateRepository)
        {
            _flowTemplateRepository = flowTemplateRepository;
        }

        public FlowTemplate Get(int id, IDbTransaction transaction = null)
        {
            var template = _flowTemplateRepository.Get(id, transaction);
            return template;
        }
    }

}