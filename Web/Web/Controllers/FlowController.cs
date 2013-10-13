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
using Flow.Library.Data.Repositories;

namespace Web.Controllers
{
    public class FlowController : Controller
    {

        public ActionResult Index()
        {
            var instance = FlowRepository.GetFlow(0,0);
            var result = NextAction(instance);
            return RedirectToAction(result.GetType().Name);
        }

        public IAction NextAction(FlowInstance instance)
        {
            var runner = new WebApiFlowRunner(instance);
            return runner.ProcessSteps();
        }

        public ActionResult CollectData()
        {
            var instance = FlowRepository.GetFlow(0,0);
            var runner = new WebApiFlowRunner(instance);
            runner.ProcessSteps();

            var brokenRules = ((DataCollectionStep) runner.NotCompleteSteps().First()).BrokenExitRules(instance.Variables);

            var form = new FormTemplateBase(brokenRules);
            return new ContentResult { Content = form.Html };
        }

        public ActionResult Submit(object[] vars)
        {

            // get the runner up to the need for data
            var instance = FlowRepository.GetFlow(0,0); // <-- new instance(!)
            var result = NextAction(instance); // <-- data collection step, hasn't been complete, will ask for variables

            // populate the flowinstance with variables that the datacollectionstep specifies
            // if available
            var variableNames = instance.Variables.Select(o => o.Key).ToList();
            foreach (var variableName in variableNames)
            {
                var value = Request.Form[variableName];

                if (value != null)
                    instance.Variables[variableName] = value;
            }

            result = NextAction(instance);

            return RedirectToAction(result.GetType().Name);
        }

        public ActionResult ShowInformationPage()
        {
            // get the current step
            var instance = FlowRepository.GetFlow(0,0);
            var result = NextAction(instance); // should be show information page step

            // populate the flowinstance with variables that the datacollectionstep specifies
            // if available
            var variableNames = instance.Variables.Select(o => o.Key).ToList();
            foreach (var variableName in variableNames)
            {
                var value = Request.Form[variableName];

                if (value != null)
                    instance.Variables[variableName] = value;
            }

            result = NextAction(instance);
            return RedirectToAction(result.GetType().Name);
        }

        public ActionResult NoAction()
        {
            return View();
        }

    }
}
