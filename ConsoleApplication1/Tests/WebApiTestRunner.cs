using System.Collections.Generic;
using System.Linq;
using Flow.Library;
using Flow.Library.Actions;
using Flow.Library.Core;
using Flow.Library.Runners;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Library.Validation.Rules;
using Xunit;

namespace Flow.Console.Tests
{
    public class WebApiTestRunner
    {
        private static FlowTemplate GetFlowTemplate()
        {
            var template = new FlowTemplate { Id = 100, Name = "Example Flow" };
            template.Variables.Add("Name", string.Empty);
            template.Variables.Add("Age", (int?) null);
            template.Steps = new List<StepBase>
            {
                new StartStepBase {Id = 0, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Start"},
                new DataCollectionStep {Id = 1, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Collect Data"},
                new StoreDataStep {Id = 3, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Store Collected Data"},
                new StepBase {Id = 4, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Send Notification Email"},
                new StopStepBase {Id = 5, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Stop"}
            };
            template.Nodes = new List<Link>
            {
                new Link {StartNode = 0, EndNode = 1},
                new Link {StartNode = 1, EndNode = 2},
                new Link {StartNode = 2, EndNode = 3},
                new Link {StartNode = 3, EndNode = 4},
                new Link {StartNode = 4, EndNode = 5}
            };
            template.RequiredSteps = new List<StepBase>
            {
                new StepBase {Name = "Start"},
                new StepBase {Name = "Stop"}
            };
            return template;
        }

        [Fact]
        public void webapi_runner_can_handle_all_steps()
        {
            // assemble
            var template = GetFlowTemplate();
            ((DataCollectionStep)template.Steps.ToList()[1]).Rules.Add(new ValidationRule { Key = "Name", Validator = new StringRequired() });

            var instance = new FlowInstance(template);
            instance.Variables["Name"] = "Some name";
            
            var runner = new WebApiFlowRunner(instance);

            // act
            var result = runner.ProcessSteps();

            // assert
            Assert.IsType<NoAction>(result);
        }

        [Fact]
        public void webapi_runner_waits_for_data_to_be_collected_by_returning_collect_data_step()
        {
            // assemble
            var template = GetFlowTemplate();
            ((DataCollectionStep)template.Steps.ToList()[1]).Rules.Add(new ValidationRule { Key = "Name", Validator = new StringRequired() });
            var instance = new FlowInstance(template);
            var runner = new WebApiFlowRunner(instance);

            // act
            var result = runner.ProcessSteps();

            // assert
            Assert.IsType<CollectData>(result);
        }

    }
}
