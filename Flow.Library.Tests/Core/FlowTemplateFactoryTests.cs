using System.Collections.Generic;
using System.Data;
using System.Linq;
using FakeItEasy;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Xunit;

namespace Flow.Library.Tests.Core
{
    public class FlowTemplateFactoryTests
    {
        [Fact]
        public void Should_return_correct_flow()
        {
            var repo = A.Fake<IRepository<FlowTemplate>>();
            A.CallTo(() => repo.Get(A<int>._)).Returns(new FlowTemplate {Id = 2, Name = "Example Template"});
            var instance = new FlowTemplateFactory(repo);

            var result = instance.Get(2);

            Assert.Equal(2, result.Id);
            Assert.Equal("Example Template", result.Name);
        }

        [Fact]
        public void Should_return_correct_steps()
        {
            var repo = A.Fake<IRepository<FlowTemplate>>();
            var steps = new List<IStep>(new List<IStep> { new StartStep(), new DataCollectionStep(), new StopStep() } );
            A.CallTo(() => repo.Get(A<int>._)).Returns(
            new FlowTemplate { Id = 2, Name = "Example Template", Steps = steps });
            var instance = new FlowTemplateFactory(repo);

            var result = instance.Get(2);

            Assert.Equal(3, result.Steps.Count);
        }

        [Fact]
        public void Should_return_correct_variables()
        {
            var repo = A.Fake<IRepository<FlowTemplate>>();
            var vars = new Dictionary<string, object> {{"Var1", null}, {"Var2", "Initial Value"}};
            A.CallTo(() => repo.Get(A<int>._)).Returns(
            new FlowTemplate { Id = 2, Name = "Example Template", Variables = vars });
            var instance = new FlowTemplateFactory(repo);

            var result = instance.Get(2);

            Assert.Equal(2, result.Variables.Count);
            Assert.Null(result.Variables["Var1"]);
            Assert.Equal("Initial Value", result.Variables["Var2"]);
        }

        [Fact]
        public void Should_return_null_if_no_matching_template_with_id()
        {
            var repo = A.Fake<IRepository<FlowTemplate>>();
            A.CallTo(() => repo.Get(A<int>._)).Returns(null);
            var vars = new Dictionary<string, object> { { "Var1", null }, { "Var2", "Initial Value" } };
            var instance = new FlowTemplateFactory(repo);

            Assert.Null(instance.Get(2));
        }
    }
}
