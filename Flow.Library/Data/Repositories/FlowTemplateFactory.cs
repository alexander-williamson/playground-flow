using System.Collections.Generic;
using Flow.Library.Core;
using Flow.Library.Steps;

namespace Flow.Library.Data.Repositories
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
            var templateResult = _flowTemplateRepository.GetFlowTemplate(id);
            var template = new FlowTemplate { Id = templateResult.Id, Name = templateResult.Name, Steps = GetSteps(templateResult.Id) };
            return template;
        }

        private List<StepBase> GetSteps(int templateId)
        {
            var stepsResult = _flowTemplateRepository.GetFlowTemplateStepsForTemplate(templateId);
            var steps = new List<StepBase>();
            foreach (var element in stepsResult)
                steps.Add(new StepBase {Id = element.Id, Name = element.Name});
            return steps;
        } 

        private IEnumerable<FlowTemplateStepRule> GetStepRules(int stepId)
        {
            var rulesResult = _flowTemplateRepository.GetFlowTemplateStepRulesForStep(stepId);
            var rules = new List<FlowTemplateStepRule>();
            foreach (var element in rulesResult)
                rules.Add(new FlowTemplateStepRule {Id = element.Id, FlowTemplateStepId = stepId, Source = element.Source});
            return rules;
        } 
    }

}