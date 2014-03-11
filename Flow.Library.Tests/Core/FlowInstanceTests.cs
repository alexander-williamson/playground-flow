using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Flow.Library.Core;
using Flow.Library.Runners;
using Flow.Library.Steps;
using Xunit;

namespace Flow.Library.Tests.Core
{
    public class FlowInstanceTests
    {
        private static FlowInstance GetMock()
        {
            var steps = new List<IStep> { new StartStep { Id = 1 }, new DataCollectionStep { Id = 2 }, new StopStep { Id = 3 } };
            var template = new FlowTemplate { Steps = steps };
            var sut = new FlowInstance { Template = template };
            sut.CompletedSteps.Add(new CompletedStep(1, 0));
            return sut;
        }

        [Fact]
        public void Should_return_correct_complete_steps_before_processing()
        {
            // arrange
            var sut = GetMock();
            
            // act
            var result = sut.CompletedSteps;

            // assert
            Assert.Equal(1, result.Count);
            Assert.Equal(1, result.First().StepId);
        }

        [Fact]
        public void Should_return_correct_next_step_if_completed_steps_are_populated()
        {
            // arrange
            var sut = GetMock();

            // act
            var result = sut.NextStep();

            // assert
            Assert.NotNull(result);
            Assert.IsType<DataCollectionStep>(result);
        }

        [Fact]
        public void Should_return_first_step_if_no_steps_are_completed()
        {
            // arrange
            var steps = new List<IStep> { new StartStep { Id = 1 }, new DataCollectionStep { Id = 2 }, new StopStep { Id = 3 } };
            var template = new FlowTemplate { Steps = steps };
            var sut = new FlowInstance { Template = template };

            // act
            var result = sut.NextStep();

            // assert
            Assert.NotNull(result);
            Assert.IsType<StartStep>(result);
        }

        [Fact]
        public void Should_return_null_if_all_steps_are_completed()
        {
            // arrange
            var sut = GetMock();
            sut.CompletedSteps.Add(new CompletedStep(1, 0));
            sut.CompletedSteps.Add(new CompletedStep(2, 0));
            sut.CompletedSteps.Add(new CompletedStep(3, 0));

            // act
            var result = sut.NextStep();

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Should_not_run_completed_step_based_on_completed_steps()
        {
            var fakeStep = A.Fake<IStep>();
            var template = new FlowTemplate { Steps = new [] { fakeStep }.ToList() };
            var sut = new FlowInstance { Template = template };
            sut.CompletedSteps.Add(new CompletedStep(1, 0));

            A.CallTo(() => fakeStep.Process(A<FlowInstance>._, A<IRunFlows>._)).MustNotHaveHappened();
        }
    }
}