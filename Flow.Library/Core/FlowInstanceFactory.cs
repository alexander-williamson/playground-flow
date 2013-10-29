using Flow.Library.Data;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Core
{
    public class FlowInstanceFactory
    {
        private readonly FlowTemplateFactory _factory;
        private readonly IFlowInstanceRepository _instanceRepostory;

        public FlowInstanceFactory(IFlowTemplateRepository templateRepository, IFlowInstanceRepository instanceRepostory)
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
