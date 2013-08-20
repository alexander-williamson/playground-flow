using System.Collections.Generic;
using Flow.Library;
using Flow.Library.Actions;
using Flow.Library.Runners;
using Flow.Library.Steps;
using Flow.Library.Validation.Rules;
using Xunit;

namespace Flow.Console.Tests
{
    public class WebApiTestRunner
    {
        private FlowInstance GetFlowInstance()
        {
            var instance = new FlowInstance { Id = 100, Name = "Example Flow" };
            instance.Variables.Add("Name", string.Empty);
            instance.Variables.Add("Age", int.MinValue);
            instance.Steps = new List<StepBase>
            {
                new StartStepBase(instance) {Id = 0, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Start"},
                new DataCollectionStep(instance) {Id = 1, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Collect Data"},
                new StoreDataStep(instance) {Id = 3, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Store Collected Data"},
                new StepBase(instance) {Id = 4, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Send Notification Email"},
                new StopStepBase(instance) {Id = 5, MaximumNodes = int.MaxValue, MinimumNodes = 1, Name = "Stop"}
            };
            instance.Nodes = new List<Link>
            {
                new Link {StartNode = 0, EndNode = 1},
                new Link {StartNode = 1, EndNode = 2},
                new Link {StartNode = 2, EndNode = 3},
                new Link {StartNode = 3, EndNode = 4},
                new Link {StartNode = 4, EndNode = 5}
            };
            instance.RequiredSteps = new List<StepBase>
            {
                new StepBase(instance) {Name = "Start"},
                new StepBase(instance) {Name = "Stop"}
            };
            return instance;
        }

        [Fact]
        public void webapi_runner_can_handle_all_steps()
        {
            // assemble
            var instance = GetFlowInstance();
            ((DataCollectionStep)instance.Steps[1]).Rules.Add(new ValidationRule { Key = "Name", Validator = new StringRequired() });
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
            var instance = GetFlowInstance();
            ((DataCollectionStep)instance.Steps[1]).Rules.Add(new ValidationRule { Key = "Name", Validator = new StringRequired() });
            var runner = new WebApiFlowRunner(instance);

            // act
            var result = runner.ProcessSteps();

            // assert
            Assert.IsType<CollectData>(result);
        }

    }
}
