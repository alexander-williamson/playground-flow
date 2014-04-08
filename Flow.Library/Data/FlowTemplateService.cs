using System;
using System.Collections.Generic;
using System.Linq;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;

namespace Flow.Library.Data
{
    public interface IFlowTemplateService
    {
        IEnumerable<Core.FlowTemplate> GetFlowTemplates(IUnitOfWork unitOfWork);
        Core.FlowTemplate GetFlowTemplate(IUnitOfWork unitOfWork, int id);
        int Add(IUnitOfWork unitOfWork, Core.FlowTemplate template);
        void Update(IUnitOfWork unitOfWork, Core.FlowTemplate template);
        void Delete(IUnitOfWork unitOfWork, Core.FlowTemplate template);
    }

    public class FlowTemplateService : IFlowTemplateService
    {
        public IEnumerable<Core.FlowTemplate> GetFlowTemplates(IUnitOfWork unitOfWork)
        {
            var templates = unitOfWork.FlowTemplates.Get().ToList();
            foreach (var template in templates)
            {
                var id = template.Id;
                var steps = unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == id).ToList();
                
                if(template.Steps == null)
                    template.Steps = new List<IStep>();

                foreach (var step in steps)
                {
                    template.Steps.Add(step);
                }
            }
            return templates;
        }

        public Core.FlowTemplate GetFlowTemplate(IUnitOfWork unitOfWork, int id)
        {
            var result = unitOfWork.FlowTemplates.Get(id);
            var steps = unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == id);

            if (result != null && result.Steps == null)
            {
                result.Steps = new List<IStep>();

                foreach (var step in steps)
                {
                    result.Steps.Add(step);
                }
            }

            return result;
        }

        public int Add(IUnitOfWork unitOfWork, Core.FlowTemplate template)
        {
            if (string.IsNullOrWhiteSpace(template.Name))
                throw new ValidationException("Template Name missing");

            var id = unitOfWork.FlowTemplates.Get().Any() ? unitOfWork.FlowTemplates.Get().Select(o => o.Id).Last() : 0;
            template.Id = ++id;
            unitOfWork.FlowTemplates.Add(template);

            if (template.Steps != null && template.Steps.Any())
            {
                foreach (var step in template.Steps)
                {
                    var stepInstance = new Core.FlowTemplateStep(step) {FlowTemplateId = id };
                    
                    // todo should not need a list
                    // todo needs an extension point

                    if (step.GetType() == typeof(StartStep)) { stepInstance.StepTypeId = 0; }
                    if (step.GetType() == typeof(StopStep)) { stepInstance.StepTypeId = 1; }
                    if (step.GetType() == typeof(CollectDataStep)) { stepInstance.StepTypeId = 2; }
                    if (step.GetType() == typeof(StoreDataStep)) { stepInstance.StepTypeId = 3; }

                    unitOfWork.FlowTemplateSteps.Add(stepInstance);
                }
            }

            unitOfWork.Commit();
            return id;
        }

        public void Update(IUnitOfWork unitOfWork, Core.FlowTemplate template)
        {
            var existing = unitOfWork.FlowTemplates.Get(template.Id);
            if (existing == null)
            {
                throw new ValidationException(String.Format("FlowTemplate Id: {0} does not exist", template.Id));
            }

            unitOfWork.FlowTemplates.Update(template.Id, template);
            if (template.Steps != null && template.Steps.Any())
            {
                foreach (var step in template.Steps)
                {
                    var stepInstance = new Core.FlowTemplateStep();
                    stepInstance.FlowTemplateId = template.Id;

                    if (step.IsDirty)
                    {
                        unitOfWork.FlowTemplateSteps.Update(step.Id, stepInstance);
                    }
                    else
                    {
                        unitOfWork.FlowTemplateSteps.Add(stepInstance);
                    }
                }
            }

            unitOfWork.Commit();
        }

        public void Delete(IUnitOfWork unitOfWork, Core.FlowTemplate template)
        {
            var steps = unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == template.Id);
            steps.ToList().ForEach(o => unitOfWork.FlowTemplateSteps.Delete(o.Id));
            unitOfWork.FlowTemplates.Delete(template.Id);
            unitOfWork.Commit();
        }
    }
}
