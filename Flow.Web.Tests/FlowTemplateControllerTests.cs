using System.Collections.Generic;
using FakeItEasy;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Web.Controllers;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;
using FlowTemplateStep = Flow.Library.Core.FlowTemplateStep;

namespace Flow.Web.Tests
{
    public class FlowTemplateControllerTests
    {

        private readonly FlowTemplateController sut;
        private readonly IUnitOfWork _unitOfWork;

        public FlowTemplateControllerTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            sut = new FlowTemplateController(_unitOfWork);
        }

        [Fact]
        public void Should_add_template()
        {
            sut.Post(new FlowTemplate {Id = 1, Name = "Test 1"});

            A.CallTo(() => _unitOfWork.FlowTemplates.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Should_return_success_on_template_post()
        {
            A.CallTo(() => _unitOfWork.FlowTemplates.Get()).Returns(new List<FlowTemplate> { new FlowTemplate { Id = 2 } });
            var result = sut.Post(new FlowTemplate { Id = 1, Name = "Test 1" });
            Assert.Equal(3, result);
        }

        [Fact]
        public void Should_throw_validation_error_if_name_missing()
        {
            Assert.Throws<ValidationException>(() => sut.Post(new FlowTemplate()));
        }

        [Fact]
        public void Should_save_child_steps()
        {
            var instance = new FlowTemplate {Name = "Example Step"};
            instance.Steps = new List<IStep> {new StartStep(), new DataCollectionStep(), new StopStep()};

            sut.Post(instance);

            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [Fact]
        public void Should_return_flow_template_by_id()
        {
            A.CallTo(() => _unitOfWork.FlowTemplates.Get(2)).Returns(new FlowTemplate {Id = 2, Name = "Template 2"});

            var result = sut.Get(2);

            Assert.Equal(2, result.Id);
            Assert.Equal("Template 2", result.Name);
        }

        [Fact]
        public void Should_return_flow_steps_when_getting_single_flow()
        {
                var steps = new List<IFlowTemplateStep>
                {
                    // TODO fix inheritance here
                    new FlowTemplateStep(new StartStep { Id = 1, Name = "Start Step 1", }) { FlowTemplateId = 1 },
                    new FlowTemplateStep(new DataCollectionStep { Id = 2, Name = "Collect Data 1"}) { FlowTemplateId = 1 },
                    new FlowTemplateStep(new StopStep { Id = 3, Name = "Steop Step 3"}) { FlowTemplateId = 1 }
                };

            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Get()).Returns(steps);
            A.CallTo(() => _unitOfWork.FlowTemplates.Get(1)).Returns(new FlowTemplate { Id = 1, Name = "Template 1" });

            var result = sut.Get(1);
            Assert.Equal(3, result.Steps.Count);
        }
    }
}
