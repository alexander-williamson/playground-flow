using System.Data.SqlClient;
using System.Web.Mvc;
using AutoMapper;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Web.Models;

namespace Web.Controllers
{
    public class FlowTemplateController : Controller
    {
        private readonly IFlowTemplateService _flowTemplateService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqlConnection _connection;
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Flow;Integrated Security=True";

        public FlowTemplateController()
        {
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
            _unitOfWork = new SqlUnitOfWork(_connection);
            _flowTemplateService =  new FlowTemplateService();
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

        public ActionResult Index()
        {
            var model = new FlowTemplateIndexViewModel();
            model.Templates = _flowTemplateService.GetFlowTemplates(_unitOfWork);
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Dto.FlowTemplate template)
        {
            var flowTemplate = Mapper.Map<Flow.Library.Core.FlowTemplate>(template);
            var id = _flowTemplateService.Add(_unitOfWork, flowTemplate);
            return RedirectToAction("Index", "FlowTemplateController", new {Success = true, Id = id});
        }

    }
}
