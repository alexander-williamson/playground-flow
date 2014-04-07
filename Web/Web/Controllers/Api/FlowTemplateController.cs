using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Web.Http;
using AutoMapper;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Web.Dto;
using Web;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Flow.Web.Controllers.Api
{
    
    public class FlowTemplateController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqlConnection _connection;
        private readonly FlowTemplateService _flowTemplateService;
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Flow;Integrated Security=True";

        public FlowTemplateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _flowTemplateService = new FlowTemplateService();
        }

        public FlowTemplateController()
        {
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
            _unitOfWork = new SqlUnitOfWork(_connection);
            _flowTemplateService = new FlowTemplateService();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }

        ~FlowTemplateController()
        {
            Dispose(false);
        }

        [NullResponseIs404]
        public FlowTemplateDto Get(int id)
        {
            var flow = _flowTemplateService.GetFlowTemplate(_unitOfWork, id);
            if (flow == null)
                return null;

            var mappedDtoFlowTemplate = Mapper.Map<FlowTemplateDto>(flow);
            return mappedDtoFlowTemplate;
        }

        public int Post(FlowTemplateDto flowTemplateDto)
        {
            var steps = new List<IStep>();
            if (flowTemplateDto.Steps != null && flowTemplateDto.Steps.Any())
            {
                steps = flowTemplateDto.Steps.Select(Map).ToList();
            }

            var template = new FlowTemplate
            {
                Id = flowTemplateDto.Id,
                Name = flowTemplateDto.Name,
                Steps = steps
            };

            var id = _flowTemplateService.Add(_unitOfWork, template);
            return id;
        }

        protected static IStep Map(FlowTemplateStepDto dto)
        {
            switch (dto.StepTypeName)
            {
                case "StartStep":
                    return Mapper.Map<StartStep>(dto);
                case "StopStep":
                    return Mapper.Map<StopStep>(dto);
                case "CollectDataStep":
                    return Mapper.Map<CollectDataStep>(dto);
                case "StoreDataStep":
                    return Mapper.Map<StoreDataStep>(dto);
            }
            throw new NotSupportedException("Unsupported Step StepTypeName provided");
        }

        public void Put(FlowTemplateDto flowTemplateDto)
        {

            var steps = new List<IStep>();
            if (flowTemplateDto.Steps != null && flowTemplateDto.Steps.Any())
            {
                //steps = flowTemplateDto.Steps.Select(Map).ToList();
                foreach (var step in flowTemplateDto.Steps)
                {
                    var mappedStep = Map(step);
                    if (step.Id > 0)
                    {
                        var match = _unitOfWork.FlowTemplateSteps.Get(mappedStep.Id);
                        if (match == null)
                        {
                            throw new ValidationException(String.Format("Step with Id {0} does not exist. Cannot update step.", step.Id));
                        }
                        mappedStep.IsDirty = true;
                    }
                    steps.Add(mappedStep);
                }
            }

            var templateResult = new FlowTemplate
            {
                Id = flowTemplateDto.Id,
                Name = flowTemplateDto.Name,
                Steps = steps,
            };

            _flowTemplateService.Update(_unitOfWork, templateResult);
        }

        public void Delete(int id)
        {
            _flowTemplateService.Delete(_unitOfWork, new FlowTemplate { Id = 1 });
        }

        public IEnumerable<FlowTemplateDto> Get()
        {
            var flowTemplates = _flowTemplateService.GetFlowTemplates(_unitOfWork).ToList();
            var mapped = flowTemplates.Select(Mapper.Map<FlowTemplateDto>).ToList();
            return mapped;
        }

    }
}
