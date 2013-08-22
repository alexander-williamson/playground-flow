using Flow.Library.Data.Repositories;

namespace Flow.Library.Core
{
    public class FlowInstanceFactory
    {
        private readonly FlowTemplateFactory _factory;
        private readonly IFlowInstanceRepostory _instanceRepostory;

        public FlowInstanceFactory(IFlowTemplateRepository templateRepository, IFlowInstanceRepostory instanceRepostory)
        {
            _factory = new FlowTemplateFactory(templateRepository);
            _instanceRepostory = instanceRepostory;
        }

        public FlowInstance NewFlowInstance(int templateId)
        {
            var template = _factory.GetTemplate(templateId);
            var instance = new FlowInstance(template);
            return instance;
        }
    }
}
