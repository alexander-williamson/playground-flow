using System.Data;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Core
{
    public class FlowTemplateFactory
    {
        private readonly IRepository<FlowTemplate> _flowTemplateRepository;
        public FlowTemplateFactory(IRepository<FlowTemplate> flowTemplateRepository)
        {
            _flowTemplateRepository = flowTemplateRepository;
        }

        public Core.FlowTemplate Get(int id, IDbTransaction transaction = null)
        {
            var template = _flowTemplateRepository.Get(id);
            return template;
        }
    }

}