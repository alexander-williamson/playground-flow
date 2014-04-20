using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Web.Dto;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Flow.Web.Controllers.Api
{
    public class FlowTemplateStepsController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly FlowTemplateService _templates;

        public FlowTemplateStepsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _templates = new FlowTemplateService();
        }

        public HttpResponseMessage Get(int parent)
        {
            var steps = _templates.GetFlowTemplateSteps(_unitOfWork, parent);
            var stepsArray = steps as IStep[] ?? steps.ToArray();

            if (!stepsArray.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var mappedSteps = stepsArray.Select(Map<FlowTemplateStepDto>).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, mappedSteps);
        }

        public HttpResponseMessage Get(int parent, int id)
        {
            var steps = _templates.GetFlowTemplateSteps(_unitOfWork, parent).ToList();
            var matching = steps.Where(o => o.Id == id).ToList();
            
            if (!matching.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);
            
            var mappedDtoFlowTemplate = Map<FlowTemplateStepDto>(matching.First());
            return Request.CreateResponse(HttpStatusCode.OK, mappedDtoFlowTemplate);
        }

        public HttpResponseMessage Delete(int parent, int id)
        {
            var parentSteps = _templates.GetFlowTemplateSteps(_unitOfWork, parent);
            var matching = parentSteps.Where(o => o.Id == id).ToList();

            if (!matching.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            _templates.Delete(_unitOfWork, new FlowTemplate { Id = id });
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(int parent, FlowTemplateStepDto instance)
        {

            try
            {
                var mapped = Map<IStep>(instance);
                var id = _templates.Add(_unitOfWork, mapped, parent);
                _unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, new { Id = id });
            }
            catch (ValidationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { ex.BrokenRules });
            }
        }

        public static T Map<T>(object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}
