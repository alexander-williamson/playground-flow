using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Flow.Library.Configuration;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;
using FlowTemplateStep = Flow.Library.Core.FlowTemplateStep;

namespace Flow.Library.Tests.Data
{
    public class FlowTemplateServiceTests
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly List<FlowTemplate> _flowTemplates;
        private readonly List<IStep> _flowTemplateSteps;
        private readonly IFlowTemplateService _flowTemplateService;

        public FlowTemplateServiceTests()
        {
            AutoMapperConfig.Configure();

            var templateRepo = A.Fake<IRepository<FlowTemplate>>();
            _flowTemplates = new List<FlowTemplate>();
            _flowTemplateSteps = new List<IStep>();
            _flowTemplateService = new FlowTemplateService();

            A.CallTo(() => templateRepo.Get()).Returns(_flowTemplates);
            A.CallTo(() => templateRepo.Add(A<FlowTemplate>._)).Invokes((FlowTemplate o) => _flowTemplates.Add(o));
            _unitofwork = A.Fake<IUnitOfWork>();
            A.CallTo(() => _unitofwork.FlowTemplates).Returns(templateRepo);
        }

        public List<IStep> FlowTemplateSteps
        {
            get { return _flowTemplateSteps; }
        }

        [Fact]
        public void Should_add_template_using_iunit_of_work()
        {
            // act
            _flowTemplateService.Add(_unitofwork, new FlowTemplate { Name = "Example Flow" });

            // assert
            A.CallTo(() => _unitofwork.FlowTemplates.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal("Example Flow", _flowTemplates.First().Name);
        }

        [Fact]
        public void Should_add_child_steps()
        {
            var instance = new FlowTemplate
            {
                Name = "Example",
                Steps = new List<IStep> {new StartStep(), new CollectDataStep(), new StoreDataStep(), new StopStep()}
            };

            _flowTemplateService.Add(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Times(4));
        }

        [Fact]
        public void Should_throw_validation_error_if_name_missing_when_adding()
        {
            Assert.Throws<ValidationException>(() => _flowTemplateService.Add(_unitofwork, new FlowTemplate()));
        }

        [Fact]
        public void Should_return_flows()
        {
            _flowTemplates.Add(new FlowTemplate {Id = 1});
            _flowTemplates.Add(new FlowTemplate {Id = 2});

            var result = _flowTemplateService.GetFlowTemplates(_unitofwork);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_return_steps_with_flows()
        {
            _flowTemplates.Add(new FlowTemplate { Id = 1 });
            _flowTemplates.Add(new FlowTemplate { Id = 2 });

            var fake = A.Fake<IFlowTemplateStep>();
            A.CallTo(() => fake.FlowTemplateId).ReturnsNextFromSequence(new [] { 1, 1, 1, 1, 2, 2 });

            A.CallTo(() => _unitofwork.FlowTemplateSteps.Get())
                .Returns(new List<IFlowTemplateStep>
                {
                    fake, fake, fake, fake
                });
            A.CallTo(() => _unitofwork.FlowTemplateSteps.Get(2))
                .Returns(fake);

            var result = _flowTemplateService.GetFlowTemplates(_unitofwork).ToArray();

            Assert.Equal(4, result.First().Steps.Count());
            Assert.Equal(2, result.Last().Steps.Count());
        }

        [Fact]
        public void Should_return_flow_by_id()
        {
            var instance = new FlowTemplate {Id = 2, Name = "Example Two"};
            A.CallTo(() => _unitofwork.FlowTemplates.Get(A<int>._)).Returns(instance);

            var result = _flowTemplateService.GetFlowTemplate(_unitofwork, 2);

            Assert.Equal(instance, result);
        }

        [Fact]
        public void Should_return_steps_with_flow_by_id()
        {
            var mock = A.Fake<IFlowTemplateStep>();
            A.CallTo(() => mock.FlowTemplateId).Returns(1);
            A.CallTo(() => _unitofwork.FlowTemplateSteps.Get())
                            .Returns(new List<IFlowTemplateStep>
                {
                    mock, mock, mock, mock
                });

            var result = _flowTemplateService.GetFlowTemplate(_unitofwork, 1);

            Assert.Equal(4, result.Steps.Count());
        }

        [Fact]
        public void Should_update_flow()
        {
            var instance = new FlowTemplate {Name = "First Value", Id = 2 };

            _flowTemplateService.Update(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplates.Update(2, A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Should_add_flow_steps_when_updating_template()
        {
            var instance = new FlowTemplate
            {
                Name = "First Value",
                Id = 2,
                Steps = new List<IStep> {A.Fake<IFlowTemplateStep>(), A.Fake<IFlowTemplateStep>()}
            };

            _flowTemplateService.Update(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Twice);
        }

        [Fact]
        public void Should_update_flow_steps_when_updating_template()
        {
            var instance = new FlowTemplate { Name = "First Value", Id = 2 };
            var mock = A.Fake<IFlowTemplateStep>();
            A.CallTo(() => mock.IsDirty).Returns(true);
            instance.Steps = new List<IStep> {mock, mock};

            _flowTemplateService.Update(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplateSteps.Update(A<int>._, A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Twice);
        }

        [Fact]
        public void Should_throw_exception_if_flow_doesnt_exist_when_updating()
        {
            var instance = new FlowTemplate {Name = "First Value", Id = 2};
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplates.Get(A<int>._)).Returns(null);

            Assert.Throws<ValidationException>(() => _flowTemplateService.Update(uow, instance));
        }

        [Fact]
        public void Should_delete_flow()
        {
            var instance = new FlowTemplate {Id = 2};
            _flowTemplateService.Delete(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplates.Delete(2)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Should_delete_child_steps()
        {
            // Arrange
            A.CallTo(() => _unitofwork.FlowTemplateSteps.Get()).Returns(new List<IFlowTemplateStep>
            {
                new FlowTemplateStep {Id = 20, FlowTemplateId = 2 }
            });
            
            // Act
            var instance = new FlowTemplate { Id = 2 };
            _flowTemplateService.Delete(_unitofwork, instance);

            // Assert
            A.CallTo(() => _unitofwork.FlowTemplates.Delete(2)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _unitofwork.FlowTemplateSteps.Delete(20)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void DeleteTemplate_should_not_throw_exception_if_no_match()
        {
            var instance = new FlowTemplate { Name = "First Value", Id = 2 };
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplates.Get(A<int>._)).Returns(null);

            Assert.DoesNotThrow(() => _flowTemplateService.Delete(uow, instance));
        }

        [Fact]
        public void Should_return_step_by_id()
        {
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplateSteps.Get(A<int>._))
                .Returns(new FlowTemplateStep {Id = 1, StepTypeId = 1, Name = "Example Step", FlowTemplateId = 1});

            Assert.Equal(1, _flowTemplateService.GetFlowTemplateStep(uow, 1).Id);
            Assert.Equal("Example Step", _flowTemplateService.GetFlowTemplateStep(uow, 1).Name);
        }

        [Fact]
        public void Should_return_null_if_step_does_not_exist()
        {
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplateSteps.Get(A<int>._)).Returns(null);

            Assert.DoesNotThrow(() => _flowTemplateService.GetFlowTemplateStep(uow, 1));
        }

        [Fact]
        public void Should_return_steps_for_flow_template()
        {
            var uow = A.Fake<IUnitOfWork>();
            var items = new List<FlowTemplateStep>
            {
                new FlowTemplateStep {Id = 1, FlowTemplateId = 1, StepTypeId = 1},
                new FlowTemplateStep {Id = 2, FlowTemplateId = 2, StepTypeId = 1},
                new FlowTemplateStep {Id = 3, FlowTemplateId = 1, StepTypeId = 2},
            };
            A.CallTo(() => uow.FlowTemplateSteps.Get()).Returns(items);

            var result = _flowTemplateService.GetFlowTemplateSteps(uow, 1);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Delete_TemplateStep_should_not_throw_exception_if_no_items()
        {
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplateSteps.Get()).Returns(null);

            Assert.DoesNotThrow(() => _flowTemplateService.Delete(uow, new FlowTemplateStep {Id = 1}));
        }

        [Fact]
        public void Delete_TemplateStep_should_delete_items_from_repo()
        {
            var uow = A.Fake<IUnitOfWork>();

            _flowTemplateService.Delete(uow, new FlowTemplateStep { Id = 1 });

            A.CallTo(() => uow.FlowTemplateSteps.Delete(1)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Should_add_StartStep_values()
        {
            var uow = A.Fake<IUnitOfWork>();
            IFlowTemplateStep inserted = null;
            A.CallTo(() => uow.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => inserted = item);

            _flowTemplateService.Add(uow, new StartStep { Name = "StartStep Example", }, 3);

            A.CallTo(() => uow.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(1, inserted.Id);
            Assert.Equal("StartStep Example", inserted.Name);
            Assert.Equal(3, inserted.FlowTemplateId);
            Assert.Equal(1, inserted.StepTypeId);
        }

        [Fact]
        public void Add_StartStep_should_increment_id()
        {
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplateSteps.Get()).Returns(
                new List<IFlowTemplateStep>
                {
                    new FlowTemplateStep {Id = 10}
                });

            var result = _flowTemplateService.Add(uow, new StartStep { Name = "StartStep Example", }, 3);

            Assert.Equal(11, result);
        }

        [Fact]
        public void Add_StopStep_should_add_values()
        {
            var uow = A.Fake<IUnitOfWork>();
            IFlowTemplateStep inserted = null;
            A.CallTo(() => uow.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => inserted = item);

            _flowTemplateService.Add(uow, new StopStep { Name = "StopStep Example", }, 2);

            A.CallTo(() => uow.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(1, inserted.Id);
            Assert.Equal("StopStep Example", inserted.Name);
            Assert.Equal(2, inserted.FlowTemplateId);
            Assert.Equal(2, inserted.StepTypeId);
        }

        [Fact]
        public void Add_StopStep_should_increment_id()
        {
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplateSteps.Get()).Returns(
                new List<IFlowTemplateStep>
                {
                    new FlowTemplateStep {Id = 5}
                });

            var result = _flowTemplateService.Add(uow, new StopStep { Name = "StopStep Example", }, 3);

            Assert.Equal(6, result);
        }

        [Fact]
        public void Add_CollectDataStep_should_add_values()
        {
            var uow = A.Fake<IUnitOfWork>();
            IFlowTemplateStep inserted = null;
            A.CallTo(() => uow.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => inserted = item);

            _flowTemplateService.Add(uow, new CollectDataStep { Name = "CollectDataStep Example", }, 2);

            A.CallTo(() => uow.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(1, inserted.Id);
            Assert.Equal("CollectDataStep Example", inserted.Name);
            Assert.Equal(2, inserted.FlowTemplateId);
            Assert.Equal(3, inserted.StepTypeId);
        }

        [Fact]
        public void Add_CollectDataStep_should_increment_id()
        {
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplateSteps.Get()).Returns(
                new List<IFlowTemplateStep>
                {
                    new FlowTemplateStep {Id = 5}
                });

            var result = _flowTemplateService.Add(uow, new CollectDataStep { Name = "CollectDataStep Example", }, 3);

            Assert.Equal(6, result);
        }

        [Fact]
        public void Add_StoreDataStep_should_add_values()
        {
            var uow = A.Fake<IUnitOfWork>();
            IFlowTemplateStep inserted = null;
            A.CallTo(() => uow.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => inserted = item);

            _flowTemplateService.Add(uow, new StoreDataStep { Name = "StoreDataStep Example", }, 2);

            A.CallTo(() => uow.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(1, inserted.Id);
            Assert.Equal("StoreDataStep Example", inserted.Name);
            Assert.Equal(2, inserted.FlowTemplateId);
            Assert.Equal(4, inserted.StepTypeId);
        }

        [Fact]
        public void Add_StoreDataStep_should_increment_id()
        {
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplateSteps.Get()).Returns(
                new List<IFlowTemplateStep>
                {
                    new FlowTemplateStep {Id = 5}
                });

            var result = _flowTemplateService.Add(uow, new StoreDataStep { Name = "StoreDataStep Example", }, 3);

            Assert.Equal(6, result);
        }
    }
}
