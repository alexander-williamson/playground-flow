using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Web.Controllers.Api
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
        public Dto.FlowTemplate Get(int id)
        {
            var flow = _flowTemplateService.GetFlowTemplate(_unitOfWork, id);
            if (flow == null)
                return null;

            var mappedDtoFlowTemplate = Mapper.Map<Dto.FlowTemplate>(flow);
            return mappedDtoFlowTemplate;
        }

        public int Post(Dto.FlowTemplate flowTemplateDto)
        {
            var template = Mapper.Map<FlowTemplate>(flowTemplateDto);
            var id = _flowTemplateService.Add(_unitOfWork, template);
            return id;
        }

        public void Put(Dto.FlowTemplate flowTemplateDto)
        {
            var template = Mapper.Map<FlowTemplate>(flowTemplateDto);
            _flowTemplateService.Update(_unitOfWork, template);
        }

        public void Delete(int id)
        {
            _flowTemplateService.Delete(_unitOfWork, new FlowTemplate { Id = 1 });
        }

        public IEnumerable<Dto.FlowTemplate> Get()
        {
            var flowTemplates = _flowTemplateService.GetFlowTemplates(_unitOfWork).ToList();
            var mapped = flowTemplates.Select(Mapper.Map<Dto.FlowTemplate>).ToList();
            return mapped;
        }

    }
}
