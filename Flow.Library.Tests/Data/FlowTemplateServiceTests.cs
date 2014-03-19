using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Flow.Library.Core;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Flow.Library.Tests.Data
{
    public class FlowTemplateServiceTests
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly List<FlowTemplate> _flowTeplateRepositoryList; 

        public FlowTemplateServiceTests()
        {
            var templateRepo = A.Fake<IRepository<FlowTemplate>>();
            _flowTeplateRepositoryList = new List<FlowTemplate>();

            A.CallTo(() => templateRepo.Get()).Returns(_flowTeplateRepositoryList);
            A.CallTo(() => templateRepo.Add(A<FlowTemplate>._)).Invokes((FlowTemplate o) => _flowTeplateRepositoryList.Add(o));
            _unitofwork = A.Fake<IUnitOfWork>();
            A.CallTo(() => _unitofwork.FlowTemplates).Returns(templateRepo);
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
        public void Should_set_first_added_item_Id_to_one()
        {
            // act
            FlowTemplateService.Add(_unitofwork, new FlowTemplate { Name = "Example Flow" });
            
            // assert
            Assert.Equal(1, _flowTeplateRepositoryList.First().Id);
        }

        [Fact]
        public void Should_increment_flow_template_id()
        {
            // act
            _flowTeplateRepositoryList.Add(new FlowTemplate {Id = 1});
            FlowTemplateService.Add(_unitofwork, new FlowTemplate { Name = "Example Flow" });

            // assert
            Assert.Equal(2, _flowTeplateRepositoryList.Last().Id);
        }

    }
}
