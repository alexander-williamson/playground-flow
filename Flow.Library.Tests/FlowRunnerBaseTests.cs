using System.Collections.Generic;
using FakeItEasy;
using Flow.Library.Core;
using Flow.Library.Runners;
using Flow.Library.Steps;
using Xunit;

namespace Flow.Library.Tests
{
    public class FlowRunnerBaseTests
    {
        private static FlowInstance GetMock()
        {
            var steps = new List<IStep> { new StartStep { Id = 1 }, new DataCollectionStep { Id = 2 }, new StopStep { Id = 3 } };
            var template = new FlowTemplate { Steps = steps };
            var sut = new FlowInstance { Template = template };
            sut.CompletedSteps.Add(new CompletedStep(1,0));
            return sut;
        }

        [Fact]
        public void Should_stop_at_step_that_cannot_be_processed_by_runner()
        {
            // assemble
            var instance = GetMock();
            var sut = new FlowRunner(instance);

            // act
            sut.ProcessSteps();
            var result = instance.NextStep();

            // assert
            Assert.NotNull(result);
            Assert.IsType<DataCollectionStep>(result);
        }

        [Fact]
        public void Should_return_true_for_supported_actions()
        {
            // assemble
            var sut = new FlowRunner(null);

            // assert
            Assert.True(sut.CanProcess(typeof(StartStep)));
            Assert.True(sut.CanProcess(typeof(StopStep)));
        }

        [Fact]
        public void Should_return_false_for_unsupported_actions()
        {
            // assemble
            var sut = new FlowRunner(null);

            // assert
            Assert.False(sut.CanProcess(typeof(DataCollectionStep)));
        }

        [Fact]
        public void Should_support_added_type()
        {
            // assemble
            var sut = new FlowRunner(null);
            sut.Types.Add(typeof(int));

            // assert
            Assert.True(sut.CanProcess(typeof(int)));
        }

        [Fact]
        public void Should_call_run_on_handleable_type()
        {
            // assemble
            var fakeStep = A.Fake<IStep>();
            A.CallTo(() => fakeStep.IsComplete).ReturnsNextFromSequence(true);
            var sut = new FlowRunner(new FlowInstance {Template = new FlowTemplate {Steps = new List<IStep> {fakeStep}}});
            sut.Types.Add(fakeStep.GetType());

            // act
            sut.ProcessSteps();

            // assert
            A.CallTo(() => fakeStep.Process(A<FlowInstance>._, A<IRunFlows>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeStep.IsComplete).MustHaveHappened(Repeated.Exactly.Once);
        }

    }
}