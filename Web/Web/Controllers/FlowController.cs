using System.Collections.Generic;
using System.Linq;
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
        public static FlowInstance GetFlow()
        {
            var template = new FlowTemplate();
            template.Variables.Add("yourName", string.Empty);
            template.Steps.Add(new DataCollectionStep
            {
                Rules =
                    new List<ValidationRule> { new ValidationRule { Key = "yourName", Validator = new StringRequired() } }
            });
            return new FlowInstance(template);;
        }

        public ActionResult Index()
        {
            var result = NextAction();
            return RedirectToAction(result.GetType().Name);
        }

        public IAction NextAction()
        {
            var instance = GetFlow();
            var runner = new WebApiFlowRunner(instance);
            return runner.ProcessSteps();
        }

        public ActionResult CollectData()
        {
            var instance = GetFlow();
            var runner = new WebApiFlowRunner(instance);
            runner.ProcessSteps();

            var brokenRules = ((DataCollectionStep) runner.NotCompleteSteps().First()).BrokenRules;

            var form = new FormTemplateBase(brokenRules);
            return new ContentResult { Content = form.Html };
        }

        public ActionResult Submit(object[] vars)
        {

            // get the runner up to the need for data
            var instance = GetFlow();
            var result = NextAction();

            // populate the flowinstance with variables that the datacollectionstep specifies
            // if available
            var variableNames = instance.Variables.Select(o => o.Key).ToList();
            foreach (var variableName in variableNames)
            {
                var value = Request.Form[variableName];

                if (value != null)
                    instance.Variables[variableName] = value;
            }

            result = NextAction();

            return RedirectToAction(result.GetType().Name);
        }

        public ActionResult NoAction()
        {
            return View();
        }

    }
}
