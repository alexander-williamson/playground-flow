using System.Collections.Generic;
using System.Web.Mvc;
using Flow.Library.Actions;
using Flow.Library.Core;
using Flow.Library.Runners;
using Flow.Library.Steps;
using Flow.Library.UI;
using Flow.Library.Validation;
using Flow.Library.Validation.Rules;

namespace Web.Controllers
{
    public class FlowController : Controller
    {
        public ActionResult Index()
        {
            var template = new FlowTemplate();
            template.Steps.Add(new DataCollectionStep
                                   {
                                       Rules =
                                           new List<ValidationRule>
                                               {new ValidationRule {Key = "yourName", Validator = new StringRequired()}}
                                   });
            var instance = new FlowInstance(template);
            var runner = new WebApiFlowRunner(instance);
            var result = runner.ProcessSteps();
            return RedirectToAction(result.GetType().Name);
        }

        public ActionResult CollectData()
        {
            var form = new FormTemplateBase();
            return new ContentResult {Content = form.TemplateHtml};
        }
        
    }
}
