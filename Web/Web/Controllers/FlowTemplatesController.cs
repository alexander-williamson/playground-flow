using System.Data.SqlClient;
using System.Web.Mvc;
using AutoMapper;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Web.Models;

namespace Flow.Web.Controllers
{
    public class FlowTemplatesController : Controller
    {
        private readonly IFlowTemplateService _flowTemplateService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqlConnection _connection;
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Flow;Integrated Security=True";

        public FlowTemplatesController()
        {
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
            _unitOfWork = new SqlUnitOfWork(_connection);
            _flowTemplateService = new FlowTemplateService(_unitOfWork);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }

        ~FlowTemplatesController()
        {
            Dispose(false);
        }

        public ActionResult Index()
        {
            var model = new FlowTemplateIndexViewModel();
            model.Templates = _flowTemplateService.GetFlowTemplates();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Dto.FlowTemplateDto templateDto)
        {
            var flowTemplate = Mapper.Map<Library.Core.FlowTemplate>(templateDto);
            var id = _flowTemplateService.Add(flowTemplate);
            return RedirectToAction("Index", "FlowTemplateController", new {Success = true, Id = id});
        }

    }
}
