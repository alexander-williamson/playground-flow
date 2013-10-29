using System.Collections.Generic;
using Flow.Library.Data.Models;

namespace Flow.Library.Data.Abstract
{
    public interface IFlowTemplateRepository
    {
        FlowTemplateDataModel GetFlowTemplate(int id);
        IEnumerable<FlowTemplateDataModel> GetFlowTemplates();
        FlowTemplateStepDataModel GetFlowTemplateStep(int id);
        IEnumerable<FlowTemplateStepDataModel> GetFlowTemplateStepsForTemplate(int templateId);
        FlowTemplateStepRuleDataModel GetFlowTemplateStepRule(int it);
        IEnumerable<FlowTemplateStepRuleDataModel> GetFlowTemplateStepRulesForStep(int stepId);
    }
}