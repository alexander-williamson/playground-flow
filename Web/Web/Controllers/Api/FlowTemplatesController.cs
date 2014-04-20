using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Web.Dto;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Flow.Web.Controllers.Api
{
    public class FlowTemplatesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FlowTemplateService _templates;

        public FlowTemplatesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _templates = new FlowTemplateService();
        }

        public IEnumerable<FlowTemplateDto> Get()
        {
            var flowTemplates = _templates.GetFlowTemplates(_unitOfWork).ToList();
            var mapped = flowTemplates.Select(Map<FlowTemplateDto>).ToList();
            return mapped;
        }

        [NullResponseIs404]
        public FlowTemplateDto Get(int id)
        {
            var flowTemplate = _templates.GetFlowTemplate(_unitOfWork, id);
            if (flowTemplate == null)
                return null;

            var flowTemplateDto = Map<FlowTemplateDto>(flowTemplate);
            return flowTemplateDto;
        }

        public HttpResponseMessage Post(FlowTemplateDto source)
        {
            var flowTemplate = Map<FlowTemplate>(source);

            if (source.Steps != null && source.Steps.Any())
            {
                flowTemplate.Steps = source.Steps.Select(Map<IStep>).ToList();
            }

            var id = _templates.Add(_unitOfWork, flowTemplate);
            return Request.CreateResponse(HttpStatusCode.Created, new {Id = id});
        }

        public void Put(FlowTemplateDto source)
        {
            var flowTemplate = new FlowTemplate
            {
                Id = source.Id,
                Name = source.Name,
            };

            if (source.Steps != null && source.Steps.Any())
            {
                flowTemplate.Steps = new List<IStep>();
                foreach (var step in source.Steps)
                {
                    var mappedStep = Map<IStep>(step);
                    if (step.Id > 0)
                    {
                        var match = _unitOfWork.FlowTemplateSteps.Get(mappedStep.Id);
                        if (match == null)
                        {
                            throw new ValidationException(String.Format("Step with Id {0} does not exist. Cannot update step.", step.Id));
                        }
                        mappedStep.IsDirty = true;
                    }
                    flowTemplate.Steps.Add(mappedStep);
                }
            }

            _templates.Update(_unitOfWork, flowTemplate);
        }

        public HttpStatusCodeResult Delete(int id)
        {
            if(_unitOfWork.FlowTemplates.Get(id) == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _templates.Delete(_unitOfWork, new FlowTemplate { Id = id });
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected static T Map<T>(object source)
        {
            try
            {
                return AutoMapper.Mapper.Map<T>(source);
            }
            catch (AutoMapper.AutoMapperMappingException ex)
            {
                throw new NotSupportedException("Step is unknown", ex);
            }
        }

    }
}
