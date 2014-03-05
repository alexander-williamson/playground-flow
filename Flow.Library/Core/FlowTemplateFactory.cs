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

        public FlowTemplate GetTemplate(int id)
        {
            var template = _flowTemplateRepository.GetTemplate(id);
            return template;
        }
    }

}