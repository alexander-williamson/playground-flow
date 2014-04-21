using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
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
        private readonly IFlowTemplateService _templates;

        public FlowTemplatesController(IFlowTemplateService flowTemplateService)
        {
            _templates = flowTemplateService;
        }

        public IEnumerable<FlowTemplateDto> Get()
        {
            var flowTemplates = _templates.GetFlowTemplates().ToList();
            var mapped = flowTemplates.Select(Map<FlowTemplateDto>).ToList();
            return mapped;
        }

        [NullResponseIs404]
        public FlowTemplateDto Get(int id)
        {
            var flowTemplate = _templates.GetFlowTemplate(id);
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

            var id = _templates.Add(flowTemplate);
            return Request.CreateResponse(HttpStatusCode.Created, new {Id = id});
        }

        public HttpResponseMessage Put(FlowTemplateDto source)
        {
            if (_templates.GetFlowTemplate(source.Id) == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            try
            {
                var flowTemplate = Map<FlowTemplate>(source);
                _templates.Update(flowTemplate);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException validationException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, validationException.Message);
            }
        }

        public HttpStatusCodeResult Delete(int id)
        {
            if(_templates.GetFlowTemplate(id) == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _templates.Delete(new FlowTemplate { Id = id });
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
