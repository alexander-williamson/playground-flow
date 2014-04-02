using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Web.Controllers
{
    public class FlowTemplateController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqlConnection _connection;
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Flow;Integrated Security=True";

        public FlowTemplateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public FlowTemplateController()
        {
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
            _unitOfWork = new SqlUnitOfWork(_connection);
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

        public IEnumerable<FlowTemplate> Get()
        {
            var result = FlowTemplateService.GetFlowTemplates(_unitOfWork).ToList();
            return result;
        }

        [NullResponseIs404Attribute]
        public FlowTemplate Get(int id)
        {
            return FlowTemplateService.GetFlowTemplate(_unitOfWork, id);
        }

        public int Post(FlowTemplate template)
        {
            var instance = template;
            var id = FlowTemplateService.Add(_unitOfWork, instance);
            return id;
        }

        public void Put(FlowTemplate template)
        {
            FlowTemplateService.Update(_unitOfWork, template);
        }

        public void Delete(int id)
        {
            FlowTemplateService.Delete(_unitOfWork, new FlowTemplate {Id = 1});
        }
    }
}
