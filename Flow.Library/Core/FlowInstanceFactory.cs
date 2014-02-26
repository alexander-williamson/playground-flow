using Flow.Library.Data.Abstract;

namespace Flow.Library.Core
{
    public class FlowInstanceFactory
    {
        private readonly IFlowTemplateRepository _templateRepository;
        public FlowInstanceFactory(IFlowTemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public FlowInstance Create(int templateId)
        {
            var template = _templateRepository.GetTemplate(templateId);
            var instance = new FlowInstance {Template = template, Variables = template.Variables};
            return instance;
        }
    }
}