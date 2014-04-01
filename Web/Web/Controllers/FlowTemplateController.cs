using System.Collections.Generic;
using System.Web.Http;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Web.Controllers
{
    public class FlowTemplateController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlowTemplateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FlowTemplate> Get()
        {
            return FlowTemplateService.GetFlowTemplates(_unitOfWork);
        }

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
