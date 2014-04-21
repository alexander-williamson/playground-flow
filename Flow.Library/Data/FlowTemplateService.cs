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
        private readonly IUnitOfWork _unitOfWork;

        public FlowTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // todo use step provider mapping
        private static T Map<T>(object source)
        {
            return Mapper.Map<T>(source);
        }

        public IEnumerable<Core.FlowTemplate> GetFlowTemplates()
        {
            var templates = _unitOfWork.FlowTemplates.Get().ToList();
            foreach (var template in templates)
            {
                var id = template.Id;
                var steps = _unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == id).ToList();

                if (template.Steps == null)
                    template.Steps = new List<IStep>();

                foreach (var step in steps)
                {
                    template.Steps.Add(Map<IStep>(step));
                }
            }
            return templates;
        }

        public Core.FlowTemplate GetFlowTemplate(int id)
        {
            var result = _unitOfWork.FlowTemplates.Get(id);
            var steps = _unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == id);

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

        public int Add(Core.FlowTemplate template)
        {
            if (string.IsNullOrWhiteSpace(template.Name))
                throw new ValidationException("Template Name missing");

            _unitOfWork.FlowTemplates.Add(template);

            if (template.Steps != null && template.Steps.Any())
            {
                foreach (var step in template.Steps)
                {
                    var stepInstance = Mapper.Map<Core.FlowTemplateStep>(step);

                    _unitOfWork.FlowTemplateSteps.Add(stepInstance);
                }
            }

            _unitOfWork.Commit();
            return template.Id;
        }

        public void Update(Core.FlowTemplate template)
        {
            var existing = _unitOfWork.FlowTemplates.Get(template.Id);
            if (existing == null)
            {
                throw new ValidationException(String.Format("FlowTemplate Id: {0} does not exist", template.Id));
            }

            _unitOfWork.FlowTemplates.Update(template.Id, template);
            if (template.Steps != null && template.Steps.Any())
            {
                foreach (var step in template.Steps)
                {
                    var stepInstance = new Core.FlowTemplateStep {FlowTemplateId = template.Id};

                    if (step.IsDirty)
                    {
                        _unitOfWork.FlowTemplateSteps.Update(step.Id, stepInstance);
                    }
                    else
                    {
                        _unitOfWork.FlowTemplateSteps.Add(stepInstance);
                    }
                }
            }

            _unitOfWork.Commit();
        }

        public void Delete(Core.FlowTemplate template)
        {
            var steps = _unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == template.Id);
            steps.ToList().ForEach(o => _unitOfWork.FlowTemplateSteps.Delete(o.Id));
            _unitOfWork.FlowTemplates.Delete(template.Id);
            _unitOfWork.Commit();
        }

        public IEnumerable<IStep> GetFlowTemplateSteps(int flowTemplateId)
        {
            var id = flowTemplateId;
            var steps = _unitOfWork.FlowTemplateSteps.Get().Where(o => o.FlowTemplateId == id).ToList();
            return steps.Select(Map<IStep>).ToList();
        }

        public IStep GetFlowTemplateStep(int id)
        {
            var step = _unitOfWork.FlowTemplateSteps.Get(id);
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

        public int Add(IStep step, int flowTemplateId)
        {
            // todo support max
            var list = _unitOfWork.FlowTemplateSteps.Get().ToList();
            var max = list.Any() ? list.Max(o => o.Id) + 1 : 1;

            var mapped = Mapper.Map<Core.FlowTemplateStep>(step);
            mapped.FlowTemplateId = flowTemplateId;
            mapped.Id = max;

            _unitOfWork.FlowTemplateSteps.Add(mapped);
            return max;
        }

        public void Update(IStep step)
        {
            throw new NotImplementedException();
        }

        public void Delete(IStep step)
        {
            _unitOfWork.FlowTemplateSteps.Delete(step.Id);
            _unitOfWork.Commit();
        }
    }
}
