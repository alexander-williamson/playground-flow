using Flow.Library.Data.Abstract;

namespace Flow.Library.Core
{
    public class FlowInstanceFactory
    {
        private readonly IRepository<FlowTemplate> _templateRepository;
        private readonly IRepository<FlowInstance> _instanceRepository;

        public FlowInstanceFactory(IRepository<FlowTemplate> templateRepository, IRepository<FlowInstance> instanceRepository)
        {
            _templateRepository = templateRepository;
            _instanceRepository = instanceRepository;
        }

        public FlowInstance Create(int templateId)
        {
            var template = _templateRepository.Get(templateId);
            var instance = new FlowInstance {Template = template, Variables = template.Variables};
            return instance;
        }

        public FlowInstance Restore(int flowId)
        {
            var instance = _instanceRepository.Get(flowId);
            var template = _templateRepository.Get(instance.Template.Id);
            instance.Template = template;

            foreach (var variable in template.Variables)
            {
                if (!instance.Variables.ContainsKey(variable.Key))
                {
                    instance.Variables.Add(variable.Key, variable.Value);
                }
            }

            return instance;
        }
    }
}