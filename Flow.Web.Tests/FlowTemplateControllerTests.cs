using System;
using System.Collections.Generic;
using System.Web.Http;
using FakeItEasy;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Web.Dto;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;
using FlowTemplateController = Flow.Web.Controllers.Api.FlowTemplateController;
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
        public void Post_should_add_template()
        {
            _sut.Post(new FlowTemplateDto {Id = 1, Name = "Test 1"});

            A.CallTo(() => _unitOfWork.FlowTemplates.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Post_success_should_return_new_id()
        {
            A.CallTo(() => _unitOfWork.FlowTemplates.Get()).Returns(new List<FlowTemplate> { new FlowTemplate { Id = 2 } });
            var result = _sut.Post(new FlowTemplateDto { Id = 1, Name = "Test 1" });
            Assert.Equal(3, result);
        }

        [Fact]
        public void Should_throw_validation_error_if_name_missing_on_post()
        {
            Assert.Throws<ValidationException>(() => _sut.Post(new FlowTemplateDto()));
        }

        [Fact]
        public void Post_Should_save_supported_child_steps()
        {
            var instance = new FlowTemplateDto {Name = "Example Step"};
            instance.Steps = new List<FlowTemplateStepDto>
            {
                new FlowTemplateStepDto { StepTypeName = "StartStep" },
                new FlowTemplateStepDto { StepTypeName = "StopStep" }
            };

            _sut.Post(instance);

            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Times(2));
        }

        [Fact]
        public void Post_Should_store_StartStep_type()
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
                    new FlowTemplateStepDto {StepTypeName = "StartStep"},
                }
            };

            // act
            _sut.Post(instance);

            // assert
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(0, captured);
        }

        [Fact]
        public void Post_Should_store_StopStep_type()
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
                    new FlowTemplateStepDto {StepTypeName = "StopStep"},
                }
            };

            // act
            _sut.Post(instance);

            // assert
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(1, captured);
        }

        [Fact]
        public void Post_Should_store_CollectDataStep_type()
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
                    new FlowTemplateStepDto {StepTypeName = "CollectDataStep"},
                }
            };

            // act
            _sut.Post(instance);

            // assert
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(2, captured);
        }

        [Fact]
        public void Post_should_store_StoreDataStep_type()
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
                    new FlowTemplateStepDto {StepTypeName = "StoreDataStep"},
                }
            };

            // act
            _sut.Post(instance);

            // assert
            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(3, captured);
        }

        [Fact]
        public void Post_should_not_store_unsupported_Step()
        {
            // assemble
            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {StepTypeName = "NonExistantStep"},
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
                new FlowTemplateStep(new StartStep { Id = 1, Name = "Start Step 1", }) { FlowTemplateId = 1, StepTypeId = 1},
                new FlowTemplateStep(new CollectDataStep { Id = 2, Name = "Collect Data 1"}) { FlowTemplateId = 1, StepTypeId = 3},
                new FlowTemplateStep(new StopStep { Id = 3, Name = "Steop Step 3"}) { FlowTemplateId = 1, StepTypeId = 2}
            };

            A.CallTo(() => _unitOfWork.FlowTemplateSteps.Get()).Returns(steps);
            A.CallTo(() => _unitOfWork.FlowTemplates.Get(1)).Returns(new FlowTemplate { Id = 1, Name = "Template 1" });

            var result = _sut.Get(1);
            Assert.Equal(3, result.Steps.Count);
        }

        [Fact]
        public void Put_template_should_update_template()
        {
            // Act
            _sut.Put(new FlowTemplateDto
            {
                Id = 2,
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Name = "Updated Step", StepTypeName = "StartStep" }
                }
            });

            // Assert
            A.CallTo(() => _unitOfWork.FlowTemplates.Update(2, A<FlowTemplate>._)).MustHaveHappened();
        }

        [Fact]
        public void Put_new_template_step_should_create_template_step()
        {
            // Assemble
            var database = A.Fake<IUnitOfWork>();
            A.CallTo(() => database.FlowTemplateSteps.Get(A<int>._)).Returns(null);
            A.CallTo(() => database.FlowTemplates.Get(A<int>._)).Returns(new FlowTemplate { Id = 1 });
            var controller = new FlowTemplateController(database);

            // Act
            controller.Put(new FlowTemplateDto
            {
                Id = 2,
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Name = "Updated Step", StepTypeName = "StartStep" }
                }
            });
            A.CallTo(() => database.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened();
        }

        [Fact]
        public void Put_existing_template_step_should_update_template_step()
        {
            // Assemble
            var database = A.Fake<IUnitOfWork>();
            A.CallTo(() => database.FlowTemplates.Get(A<int>._)).Returns(new FlowTemplate { Id = 1 });
            A.CallTo(() => database.FlowTemplateSteps.Get(0)).Returns(null);
            A.CallTo(() => database.FlowTemplateSteps.Get(10))
                .Returns(new FlowTemplateStep {Id = 10, StepTypeId = 1, FlowTemplateId = 1});
            FlowTemplateStep captured = null;
            A.CallTo(() => database.FlowTemplateSteps.Update(A<int>._, A<IFlowTemplateStep>._))
                .Invokes(f =>
                {
                    captured = (FlowTemplateStep)f.Arguments[1];
                });

            // Act
            var controller = new FlowTemplateController(database);
            controller.Put(new FlowTemplateDto
            {
                Id = 1,
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Id = 10, Name = "Updated Step", StepTypeName = "StartStep" }
                }
            });
            A.CallTo(() => database.FlowTemplateSteps.Update(10, A<IFlowTemplateStep>._)).MustHaveHappened();
            Assert.Equal(1, captured.FlowTemplateId);
        }

        [Fact]
        public void Put_should_throw_exception_if_flow_template_step_does_not_exist_when_id_set()
        {
            // Assemble
            var database = A.Fake<IUnitOfWork>();
            A.CallTo(() => database.FlowTemplates.Get(A<int>._)).Returns(new FlowTemplate {Id = 1});
            A.CallTo(() => database.FlowTemplateSteps.Get(A<int>._)).Returns(null);
            var controller = new FlowTemplateController(database);

            // Assert
            Assert.Throws<ValidationException>(() => controller.Put(new FlowTemplateDto
            {
                Steps = new List<FlowTemplateStepDto> {new FlowTemplateStepDto
                {
                    Id = 10,
                    StepTypeName = "StartStep"
                }}
            }));
        }

        [Fact]
        public void Put_should_throw_exception_if_flow_template_does_not_exist()
        {
            // Assemble
            var database = A.Fake<IUnitOfWork>();
            A.CallTo(() => database.FlowTemplates.Get(A<int>._)).Returns(null);

            // Act
            var controller = new FlowTemplateController(database);

            // Assert
            Assert.Throws<ValidationException>(() => controller.Put(new FlowTemplateDto {Id = 1, Name = "Example"}));
        }

        [Fact]
        public void Delete_should_return_not_found_if_not_exists()
        {
            // Assemble
            var database = A.Fake<IUnitOfWork>();
            A.CallTo(() => database.FlowTemplates.Get(A<int>._)).Returns(null);

            // Act
            var controller = new FlowTemplateController(database);
            
            // Assert
            Assert.Throws<HttpResponseException>(() => controller.Delete(100));
        }

        [Fact]
        public void Delete_should_request_item_to_be_deleted_from_repository()
        {
            var database = A.Fake<IUnitOfWork>();
            A.CallTo(() => database.FlowTemplates.Get(A<int>._)).Returns(new FlowTemplate {Id = 1, Name = "Example"});
            A.CallTo(() => database.FlowTemplateSteps.Get()).Returns(new List<FlowTemplateStep>
            {
                new FlowTemplateStep {Id = 10, FlowTemplateId = 1},
                new FlowTemplateStep {Id = 20, FlowTemplateId = 1},
                new FlowTemplateStep {Id = 30, FlowTemplateId = 1}
            });

            // Act
            var controller = new FlowTemplateController(database);
            controller.Delete(1);

            // Assert
            A.CallTo(() => database.FlowTemplates.Delete(1)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => database.FlowTemplateSteps.Delete(10)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => database.FlowTemplateSteps.Delete(20)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => database.FlowTemplateSteps.Delete(30)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
