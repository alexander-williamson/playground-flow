using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Web.Dto;
using FlowTemplate = Flow.Library.Core.FlowTemplate;
using FlowTemplateStep = Flow.Library.Core.FlowTemplateStep;

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

        public HttpResponseMessage Get(int parent)
        {
            var steps = _flowTemplateService.GetFlowTemplateSteps(_unitOfWork, parent);
            var stepsArray = steps as IStep[] ?? steps.ToArray();

            if (steps == null || !stepsArray.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var mappedSteps = stepsArray.Select(Mapper.Map<FlowTemplateStepDto>).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, mappedSteps);
        }

        public HttpResponseMessage Get(int parent, int id)
        {
            var steps = _flowTemplateService.GetFlowTemplateSteps(_unitOfWork, parent).ToList();
            var matching = steps.Where(o => o.Id == id).ToList();
            
            if (!matching.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);
            
            var mappedDtoFlowTemplate = AutoMapper.Mapper.Map<FlowTemplateStepDto>(matching.First());
            return Request.CreateResponse(HttpStatusCode.OK, mappedDtoFlowTemplate);
        }

        public HttpResponseMessage Delete(int parent, int id)
        {
            var parentSteps = _flowTemplateService.GetFlowTemplateSteps(_unitOfWork, parent);
            var matching = parentSteps.Where(o => o.Id == id).ToList();

            if (!matching.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            _flowTemplateService.Delete(_unitOfWork, new FlowTemplate { Id = id });
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(int parent, FlowTemplateStepDto instance)
        {

            try
            {
                var mapped = Mapper.Map<IStep>(instance);
                var id = _flowTemplateService.Add(_unitOfWork, mapped, parent);
                _unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, new { Id = id });
            }
            catch (ValidationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { ex.BrokenRules });
            }
        }
    }
}
