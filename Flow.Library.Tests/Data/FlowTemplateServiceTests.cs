using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Flow.Library.Tests.Data
{
    public class FlowTemplateServiceTests
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly List<FlowTemplate> _flowTeplateRepositoryList;
        private readonly List<IStep> _flowTemplateSteps;

        public FlowTemplateServiceTests()
        {
            var templateRepo = A.Fake<IRepository<FlowTemplate>>();
            _flowTeplateRepositoryList = new List<FlowTemplate>();
            _flowTemplateSteps = new List<IStep>();

            A.CallTo(() => templateRepo.Get()).Returns(_flowTeplateRepositoryList);
            A.CallTo(() => templateRepo.Add(A<FlowTemplate>._)).Invokes((FlowTemplate o) => _flowTeplateRepositoryList.Add(o));
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
            FlowTemplateService.Add(_unitofwork, new FlowTemplate { Name = "Example Flow" });

            // assert
            A.CallTo(() => _unitofwork.FlowTemplates.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal("Example Flow", _flowTeplateRepositoryList.First().Name);
        }

        [Fact]
        public void Should_add_child_steps()
        {
            var instance = new FlowTemplate {Name = "Example"};
            instance.Steps = new List<IStep>();
            instance.Steps.Add(A.Fake<IFlowTemplateStep>());
            instance.Steps.Add(A.Fake<IFlowTemplateStep>());
            instance.Steps.Add(A.Fake<IFlowTemplateStep>());
            instance.Steps.Add(A.Fake<IFlowTemplateStep>());

            FlowTemplateService.Add(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Times(4));
        }

        [Fact]
        public void Should_throw_validation_error_if_name_missing_when_adding()
        {
            Assert.Throws<ValidationException>(() => FlowTemplateService.Add(_unitofwork, new FlowTemplate()));
        }

        [Fact]
        public void Should_return_flows()
        {
            _flowTeplateRepositoryList.Add(new FlowTemplate {Id = 1});
            _flowTeplateRepositoryList.Add(new FlowTemplate {Id = 2});

            var result = FlowTemplateService.GetFlowTemplates(_unitofwork);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_return_steps_with_flows()
        {
            _flowTeplateRepositoryList.Add(new FlowTemplate { Id = 1 });
            _flowTeplateRepositoryList.Add(new FlowTemplate { Id = 2 });

            var fake = A.Fake<IFlowTemplateStep>();
            A.CallTo(() => fake.FlowTemplateId).ReturnsNextFromSequence(new [] { 1, 1, 1, 1, 2, 2 });

            A.CallTo(() => _unitofwork.FlowTemplateSteps.Get())
                .Returns(new List<IFlowTemplateStep>
                {
                    fake, fake, fake, fake
                });
            A.CallTo(() => _unitofwork.FlowTemplateSteps.Get(2))
                .Returns(fake);

            var result = FlowTemplateService.GetFlowTemplates(_unitofwork).ToArray();

            Assert.Equal(4, result.First().Steps.Count());
            Assert.Equal(2, result.Last().Steps.Count());
        }

        [Fact]
        public void Should_return_flow_by_id()
        {
            var instance = new FlowTemplate {Id = 2, Name = "Example Two"};
            A.CallTo(() => _unitofwork.FlowTemplates.Get(A<int>._)).Returns(instance);

            var result = FlowTemplateService.GetFlowTemplate(_unitofwork, 2);

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

            var result = FlowTemplateService.GetFlowTemplate(_unitofwork, 1);

            Assert.Equal(4, result.Steps.Count());
        }

        [Fact]
        public void Should_update_flow()
        {
            var instance = new FlowTemplate {Name = "First Value", Id = 2 };

            FlowTemplateService.Update(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplates.Update(2, A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Should_add_flow_steps_when_updating_template()
        {
            var instance = new FlowTemplate { Name = "First Value", Id = 2 };
            instance.Steps = new List<IStep>();
            instance.Steps.Add(A.Fake<IFlowTemplateStep>());
            instance.Steps.Add(A.Fake<IFlowTemplateStep>());

            FlowTemplateService.Update(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Twice);
        }

        [Fact]
        public void Should_update_flow_steps_when_updating_template()
        {
            var instance = new FlowTemplate { Name = "First Value", Id = 2 };
            var mock = A.Fake<IFlowTemplateStep>();
            A.CallTo(() => mock.IsDirty).Returns(true);
            instance.Steps = new List<IStep>();
            instance.Steps.Add(mock);
            instance.Steps.Add(mock);

            FlowTemplateService.Update(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplateSteps.Update(A<int>._, A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Twice);
        }

        [Fact]
        public void Should_throw_exception_if_flow_doesnt_exist_when_updating()
        {
            var instance = new FlowTemplate {Name = "First Value", Id = 2};
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplates.Get(A<int>._)).Returns(null);

            Assert.Throws<ValidationException>(() => FlowTemplateService.Update(uow, instance));
        }

        [Fact]
        public void Should_delete_flow()
        {
            var instance = new FlowTemplate {Id = 2};
            FlowTemplateService.Delete(_unitofwork, instance);

            A.CallTo(() => _unitofwork.FlowTemplates.Delete(2)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Should_throw_exception_if_flow_doesnt_exist_when_deleting()
        {
            var instance = new FlowTemplate { Name = "First Value", Id = 2 };
            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.FlowTemplates.Get(A<int>._)).Returns(null);

            Assert.Throws<ValidationException>(() => FlowTemplateService.Update(uow, instance));
        }
    }
}
