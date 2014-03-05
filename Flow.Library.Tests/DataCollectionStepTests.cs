using System.Collections.Generic;
using Flow.Library.Core;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Library.Validation.Rules;
using Xunit;

namespace Flow.Library.Tests
{
    public class DataCollectionStepTests
    {
        [Fact]
        public void Should_return_false_if_not_processed()
        {
            // assemble
            var sut = new DataCollectionStep();

            // assert
            Assert.False(sut.CanContinue);
            Assert.False(sut.IsProcessed);
        }

        [Fact]
        public void Should_return_true_when_no_rules_to_validate_and_processed()
        {
            // assemble
            var sut = new DataCollectionStep();

            // act
            sut.Process(new FlowInstance(), null);

            // assert
            Assert.True(sut.CanContinue);
            Assert.True(sut.IsProcessed);
        }

        [Fact]
        public void Should_require_data_collection_if_exit_rule_invalid()
        {
            // assemble
            var sut = new DataCollectionStep(new List<IValidationRule>(new[] {new StringRequired()}));

            // act
            sut.Process(new FlowInstance(), null);

            // assert
            Assert.False(sut.CanContinue);
            Assert.True(sut.IsProcessed);
        }

        [Fact]
        public void Should_continue_when_rules_statisfied()
        {
            // assemble
            var flow = new FlowInstance();
            var sut = new DataCollectionStep(new List<IValidationRule>(new[] { new StringRequired{ VariableKey = "Variable1"} }));

            // act
            sut.Process(flow, null);
            flow.Variables.Add("Variable1","");
            sut.Process(flow, null);

            // assert
            Assert.False(sut.CanContinue);
            Assert.True(sut.IsProcessed);
        }

        [Fact]
        public void Should_not_update_flow_variables_if_variables_do_not_exist()
        {
            // assemble
            var flow = new FlowInstance();
            var sut = new DataCollectionStep(new List<IValidationRule>(new[] { new StringRequired { VariableKey = "Variable1" } }));

            // act
            sut.Process(flow, null);
            sut.Process(flow, null);

            // assert
            Assert.Equal(0, flow.Variables.Count);
        }
    }
}
