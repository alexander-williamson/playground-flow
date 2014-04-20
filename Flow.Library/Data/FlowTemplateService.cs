using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;

namespace Flow.Library.Data
{
    public class FlowTemplateService : IFlowTemplateService
    {
        // todo use step provider mapping
        private static T Map<T>(object source)
        {
            return Mapper.Map<T>(source);
        }

        public IEnumerable<Core.FlowTemplate> GetFlowTemplates(IUnitOfWork unitOfWork)
        {
            var templates = unitOfWork.FlowTemplates.Get().ToList();
            foreach (var template in templates)
            {
                var id = template.Id;
                var steps = unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == id).ToList();

                if (template.Steps == null)
                    template.Steps = new List<IStep>();

                foreach (var step in steps)
                {
                    template.Steps.Add(Map<IStep>(step));
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
                    result.Steps.Add(Map<IStep>(step));
                }
            }

            return result;
        }

        public int Add(IUnitOfWork unitOfWork, Core.FlowTemplate template)
        {
            if (string.IsNullOrWhiteSpace(template.Name))
                throw new ValidationException("Template Name missing");

            template.Id = GetNextId(unitOfWork);
            unitOfWork.FlowTemplates.Add(template);

            if (template.Steps != null && template.Steps.Any())
            {
                foreach (var step in template.Steps)
                {
                    var stepInstance = Mapper.Map<Core.FlowTemplateStep>(step);

                    unitOfWork.FlowTemplateSteps.Add(stepInstance);
                }
            }

            unitOfWork.Commit();
            return template.Id;
        }

        private static int GetNextId(IUnitOfWork unitOfWork)
        {
            var id = unitOfWork.FlowTemplates.Get().Any() ? unitOfWork.FlowTemplates.Get().Select(o => o.Id).Last() : 0;
            return id + 1;
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

        public IEnumerable<IStep> GetFlowTemplateSteps(IUnitOfWork unitOfWork, int flowTemplateId)
        {
            var id = flowTemplateId;
            var steps = unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == id).ToList();
            return steps.Select(Map<IStep>).ToList();
        }

        public IStep GetFlowTemplateStep(IUnitOfWork unitOfWork, int id)
        {
            var step = unitOfWork.FlowTemplateSteps.Get(id);
            if (step == null)
                return null;

            var result = new Core.FlowTemplateStep
            {
                Id = step.Id,
                Name = step.Name,
                FlowTemplateId = step.FlowTemplateId,
                StepTypeId = step.StepTypeId
            };
            return result;
        }

        public int Add(IUnitOfWork unitOfWork, IStep step, int flowTemplateId)
        {
            // todo support max
            var list = unitOfWork.FlowTemplateSteps.Get().ToList();
            var max = list.Any() ? list.Max(o => o.Id) + 1 : 1;

            var mapped = Mapper.Map<Core.FlowTemplateStep>(step);
            mapped.FlowTemplateId = flowTemplateId;
            mapped.Id = max;

            unitOfWork.FlowTemplateSteps.Add(mapped);
            return max;
        }

        public void Update(IUnitOfWork unitOfWork, IStep step)
        {
            throw new NotImplementedException();
        }

        public void Delete(IUnitOfWork unitOfWork, IStep step)
        {
            unitOfWork.FlowTemplateSteps.Delete(step.Id);
            unitOfWork.Commit();
        }
    }
}
