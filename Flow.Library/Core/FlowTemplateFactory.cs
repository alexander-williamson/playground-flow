using System.Collections.Generic;
using System.Linq;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;

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
            var templateResult = _flowTemplateRepository.GetFlowTemplate(id);
            var template = new FlowTemplate { Id = templateResult.Id, Name = templateResult.Name, Steps = GetSteps(templateResult.Id) };
            return template;
        }

        private List<StepBase> GetSteps(int templateId)
        {
            var stepsResult = _flowTemplateRepository.GetFlowTemplateStepsForTemplate(templateId);
            var steps = new List<StepBase>();
            foreach (var element in stepsResult)
            {
                var entryRules = GetStepEntryRules(element.Id).ToList();
                var exitRules = GetStepExitRules(element.Id).ToList();
                steps.Add(new StepBase { Id = element.Id, Name = element.Name, EntryRules = entryRules, ExitRules = exitRules});
            }
            return steps;
        }

        private IEnumerable<ValidationRule> GetStepEntryRules(int stepId)
        {
            //var rulesResult = _flowTemplateRepository.GetFlowTemplateStepRulesForStep(stepId);
            //var rules = new List<FlowTemplateStepRule>();
            //foreach (var element in rulesResult)
            //    rules.Add(new FlowTemplateStepRule {Id = element.Id, FlowTemplateStepId = stepId, Source = element.Source});
            //return rules;
            return new List<ValidationRule>();
        }

        private IEnumerable<ValidationRule> GetStepExitRules(int stepId)
        {
            //var rulesResult = _flowTemplateRepository.GetFlowTemplateStepRulesForStep(stepId);
            //var rules = new List<FlowTemplateStepRule>();
            //foreach (var element in rulesResult)
            //    rules.Add(new FlowTemplateStepRule { Id = element.Id, FlowTemplateStepId = stepId, Source = element.Source });
            //return rules;
            return new List<ValidationRule>();
        }
    }

}