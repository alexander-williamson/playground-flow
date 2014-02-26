using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Xunit;

namespace Flow.Library.Tests
{
    public class FlowInstanceFactoryTests
    {
        [Fact]
        public void Should_create_new_flow_from_template()
        {
            // assemble
            const string templateName = @"Example Template";
            var templateRepository = A.Fake<IFlowTemplateRepository>();
            A.CallTo(() => templateRepository.GetTemplate(A<int>._)).Returns(new FlowTemplate { Id = 2, Name = templateName });
            
            // act
            var sut = new FlowInstanceFactory(templateRepository);
            var instance = sut.Create(1);

            // assert
            Assert.Equal(2, instance.Template.Id);
            Assert.Equal(templateName, instance.Template.Name);
        }

        [Fact]
        public void Should_create_new_object_with_steps_not_run()
        {
            // assemble
            const string templateName = @"Example Template";
            var templateRepository = A.Fake<IFlowTemplateRepository>();
            var steps = new List<StepBase> {new StartStep(), new StopStep()};
            A.CallTo(() => templateRepository.GetTemplate(A<int>._)).Returns(new FlowTemplate
                                                                                     {
                                                                                         Id = 2, 
                                                                                         Name = templateName, 
                                                                                         Steps = steps
                                                                                     });

            // act
            var sut = new FlowInstanceFactory(templateRepository);
            var instance = sut.Create(1);

            // assert
            Assert.Equal(2, instance.Template.Id);
            Assert.Equal(templateName, instance.Template.Name);
            Assert.Equal(0, instance.CompletedSteps.Count);
        }

        [Fact]
        public void Should_create_new_object_with_correct_variables()
        {
            // assemble
            var templateRepository = A.Fake<IFlowTemplateRepository>();
            var steps = new List<StepBase> { new StartStep(), new StopStep() };
            var vars = new Dictionary<string, object> {{"Example Key 1", "Example Value 1"}, {"Example Key 2", 2}};
            A.CallTo(() => templateRepository.GetTemplate(A<int>._)).Returns(new FlowTemplate { Id = 2, Steps = steps, Variables = vars });

            // act
            var instance = new FlowInstanceFactory(templateRepository);
            var sut = instance.Create(1).Variables.ToList();

            // assert
            Assert.Equal(2, sut.Count);
            Assert.Equal("Example Key 1", sut[0].Key);
            Assert.Equal("Example Value 1", sut[0].Value);
            Assert.Equal("Example Key 2", sut[1].Key);
            Assert.Equal(2, sut[1].Value);
        }
        
    }
}
