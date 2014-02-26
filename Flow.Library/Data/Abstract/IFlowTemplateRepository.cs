using System.Collections.Generic;
using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowTemplateRepository
    {
        FlowTemplate GetTemplate(int id);
        IEnumerable<FlowTemplate> GetTemplates();
        FlowTemplateStep GetTemplateStep(int id);
        IEnumerable<FlowTemplateStep> GetTemplateStepsForTemplate(int templateId);
        FlowTemplateStepRule GetTemplateStepRule(int it);
        IEnumerable<FlowTemplateStepRule> GetTemplateStepRulesForStep(int stepId);
    }
}