using System.Collections.Generic;
using Flow.Library.Data.Models;

namespace Flow.Library
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
    public interface IFlowInstanceRepostory
    {
        FlowInstanceDataModel GetFlowInstance(int id);
        bool FlowInstanceExists(int id);
    }

    public class FlowInstanceDataModel
    {
        public int Id { get; set; }
        public int FlowTemplateId { get; set; }
    }
}