using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Mvc;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Web.Dto;

namespace Flow.Web.Controllers.Api
{
    public class FlowTemplateStepsController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly FlowTemplateService _flowTemplateService;

        public FlowTemplateStepsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _flowTemplateService = new FlowTemplateService();
        }

        [NullResponseIs404]
        public HttpResponseMessage Get(int id)
        {
            var step = _flowTemplateService.GetFlowTemplateStep(_unitOfWork, id);
            if (step == null)
                return null;

            var mappedDtoFlowTemplate = AutoMapper.Mapper.Map<FlowTemplateStepDto>(step);
            return Request.CreateResponse(HttpStatusCode.OK, mappedDtoFlowTemplate);
        }

        [NullResponseIs404]
        public HttpResponseMessage Get(int parent, int id)
        {
            var step = _flowTemplateService.GetFlowTemplateStep(_unitOfWork, id);
            if (step == null)
                return null;

            var mappedDtoFlowTemplate = AutoMapper.Mapper.Map<FlowTemplateStepDto>(step);
            return Request.CreateResponse(HttpStatusCode.OK, mappedDtoFlowTemplate);
        }


    }
}
