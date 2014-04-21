using System.Collections.Generic;
using Flow.Library.Steps;

namespace Flow.Library.Data
{
    public interface IFlowTemplateService
    {
        IEnumerable<Core.FlowTemplate> GetFlowTemplates();
        Core.FlowTemplate GetFlowTemplate(int id);
        int Add(Core.FlowTemplate template);
        void Update(Core.FlowTemplate template);
        void Delete(Core.FlowTemplate template);

        IEnumerable<IStep> GetFlowTemplateSteps(int flowTemplateId);
        IStep GetFlowTemplateStep(int id);
        int Add(IStep step, int flowTemplateId);
        void Update(IStep step);
        void Delete(IStep step);
    }
}