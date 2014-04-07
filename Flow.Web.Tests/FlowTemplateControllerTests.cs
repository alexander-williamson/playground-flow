using System;
using System.Collections.Generic;
using FakeItEasy;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Web;
using Web.Dto;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;
using FlowTemplateController = Web.Controllers.Api.FlowTemplateController;
using FlowTemplateStep = Flow.Library.Core.FlowTemplateStep;

namespace Flow.Web.Tests
{
    public class FlowTemplateControllerTests
    {
        private readonly FlowTemplateController _sut;
        private readonly IUnitOfWork _unitOfWork;

        public FlowTemplateControllerTests()
        {
            AutoMapperConfig.Configure();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _sut = new FlowTemplateController(_unitOfWork);
        }

        [Fact]
        public void Should_add_template()
        {
            _sut.Post(new FlowTemplateDto {Id = 1, Name = "Test 1"});

            A.CallTo(() => _unitOfWork.FlowTemplates.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Should_return_success_on_template_post()
        {
            A.CallTo(() => _unitOfWork.FlowTemplates.Get()).Returns(new List<FlowTemplate> { new FlowTemplate { Id = 2 } });
            var result = _sut.Post(new FlowTemplateDto { Id = 1, Name = "Test 1" });
            Assert.Equal(3, result);
        }

        [Fact]
        public void Should_throw_validation_error_if_name_missing()
        {
            Assert.Throws<ValidationException>(() => _sut.Post(new FlowTemplateDto()));
        }

        [Fact]
        public void Should_save_supported_child_steps()
        {
            var instance = new FlowTemplateDto {Name = "Example Step"};
            instance.Steps = new List<FlowTemplateStepDto>
            {
                new FlowTemplateStepDto { Type = "StartStep" },
                new FlowTemplateStepDto { Type = "StopStep" }
            };

            _sut.Post(instance);

            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Times(2));
        }

        [Fact]
        public void Should_store_StartStep_type()
        {
            // assemble
            int captured = -1;
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => captured = item.StepTypeId);

            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Type = "StartStep"},
                }
            };

            // act
            _sut.Post(instance);

            // assert
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(0, captured);
        }

        [Fact]
        public void Should_store_StopStep_type()
        {
            // assemble
            int captured = 0;
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => captured = item.StepTypeId);

            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Type = "StopStep"},
                }
            };

            // act
            _sut.Post(instance);

            // assert
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(1, captured);
        }

        [Fact]
        public void Should_store_CollectDataStep_type()
        {
            // assemble
            int captured = 0;
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => captured = item.StepTypeId);

            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Type = "CollectDataStep"},
                }
            };

            // act
            _sut.Post(instance);

            // assert
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(2, captured);
        }

        [Fact]
        public void Should_store_StoreDataStep_type()
        {
            // assemble
            int captured = 0;
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => captured = item.StepTypeId);

            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Type = "StoreDataStep"},
                }
            };

            // act
            _sut.Post(instance);

            // assert
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(3, captured);
        }

        [Fact]
        public void Should_not_store_unsupported_Step()
        {
            // assemble
            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Type = "NonExistantStep"},
                }
            };

            // assert
            Assert.Throws<NotSupportedException>(() => _sut.Post(instance));
        }

        [Fact]
        public void Should_return_flow_template_by_id()
        {
            A.CallTo(() => _unitOfWork.FlowTemplates.Get(2)).Returns(new FlowTemplate {Id = 2, Name = "Template 2"});

            var result = _sut.Get(2);

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
                    new FlowTemplateStep(new CollectDataStep { Id = 2, Name = "Collect Data 1"}) { FlowTemplateId = 1 },
                    new FlowTemplateStep(new StopStep { Id = 3, Name = "Steop Step 3"}) { FlowTemplateId = 1 }
                };

            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Get()).Returns(steps);
            A.CallTo(() => _unitOfWork.FlowTemplates.Get(1)).Returns(new FlowTemplate { Id = 1, Name = "Template 1" });

            var result = _sut.Get(1);
            Assert.Equal(3, result.Steps.Count);
        }

    }
}
