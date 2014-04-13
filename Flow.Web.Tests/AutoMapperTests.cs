using AutoMapper;
using Flow.Library.Steps;
using Flow.Web.Dto;
using Xunit;

namespace Flow.Web.Tests
{
    public class AutoMapperTests
    {
        public AutoMapperTests()
        {
            AutoMapperConfig.Configure();
        }

        [Fact]
        public void StartStep_should_map_value_types_to_FlowTemplateStepDto()
        {
            var step = new StartStep {Id = 1, Name = "Example StartStep"};

            var result = Mapper.Map<FlowTemplateStepDto>(step);

            Assert.IsType<FlowTemplateStepDto>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Example StartStep", result.Name);
            Assert.Equal("StartStep", result.StepTypeName);
        }

        [Fact]
        public void StopStep_should_map_value_types_to_FlowTemplateStepDto()
        {
            var step = new StopStep { Id = 2, Name = "Example StopStep" };

            var result = Mapper.Map<FlowTemplateStepDto>(step);

            Assert.IsType<FlowTemplateStepDto>(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("Example StopStep", result.Name);
            Assert.Equal("StopStep", result.StepTypeName);
        }

        [Fact]
        public void CollectDataStep_should_map_value_types_to_FlowTemplateStepDto()
        {
            var step = new CollectDataStep { Id = 3, Name = "Example CollectDataStep" };

            var result = Mapper.Map<FlowTemplateStepDto>(step);

            Assert.IsType<FlowTemplateStepDto>(result);
            Assert.Equal(3, result.Id);
            Assert.Equal("Example CollectDataStep", result.Name);
            Assert.Equal("CollectDataStep", result.StepTypeName);
        }

        [Fact]
        public void StoreDataStep_should_map_value_types_to_FlowTemplateStepDto()
        {
            var step = new StoreDataStep { Id = 4, Name = "Example StoreDataStep" };

            var result = Mapper.Map<FlowTemplateStepDto>(step);

            Assert.IsType<FlowTemplateStepDto>(result);
            Assert.Equal(4, result.Id);
            Assert.Equal("Example StoreDataStep", result.Name);
            Assert.Equal("StoreDataStep", result.StepTypeName);
        }

        [Fact]
        public void FlowTemplateStepDto_should_map_values_to_StartStep()
        {
            var data = new FlowTemplateStepDto
            {
                Id = 1,
                Name = "StartStep Example",
                StepTypeName = "StartStep"
            };

            var result = Mapper.Map<StartStep>(data);

            Assert.IsType<StartStep>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("StartStep Example", result.Name);
        }

        [Fact]
        public void FlowTemplateStepDto_should_map_values_to_StopStep()
        {
            var data = new FlowTemplateStepDto
            {
                Id = 1,
                Name = "StopStep Example",
                StepTypeName = "StopStep"
            };

            var result = Mapper.Map<StopStep>(data);

            Assert.IsType<StopStep>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("StopStep Example", result.Name);
        }

        [Fact]
        public void FlowTemplateStepDto_should_map_values_to_CollectDataStep()
        {
            var data = new FlowTemplateStepDto
            {
                Id = 1,
                Name = "CollectDataStep Example",
                StepTypeName = "CollectDataStep"
            };

            var result = Mapper.Map<CollectDataStep>(data);

            Assert.IsType<CollectDataStep>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("CollectDataStep Example", result.Name);
        }

        [Fact]
        public void FlowTemplateStepDto_should_map_values_to_StoreDataStep()
        {
            var data = new FlowTemplateStepDto
            {
                Id = 1,
                Name = "StoreDataStep Example",
                StepTypeName = "StoreDataStep"
            };

            var result = Mapper.Map<StoreDataStep>(data);

            Assert.IsType<StoreDataStep>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("StoreDataStep Example", result.Name);
        }

        [Fact]
        public void FlowTemplateStepDto_should_map_to_StartStep_when_mapped_to_IStep()
        {
            var origin = new FlowTemplateStepDto {Name = "StartStep Example", StepTypeName = "StartStep"};

            var result = Mapper.Map<IStep>(origin);

            Assert.IsType<StartStep>(result);
        }

        [Fact]
        public void FlowTemplateStepDto_should_map_to_StopStep_when_mapped_to_IStep()
        {
            var origin = new FlowTemplateStepDto { Name = "StopStep Example", StepTypeName = "StopStep" };

            var result = Mapper.Map<IStep>(origin);

            Assert.IsType<StopStep>(result);
        }

        [Fact]
        public void FlowTemplateStepDto_should_map_to_CollectDataStep_when_mapped_to_IStep()
        {
            var origin = new FlowTemplateStepDto { Name = "CollectDataStep Example", StepTypeName = "CollectDataStep" };

            var result = Mapper.Map<IStep>(origin);

            Assert.IsType<CollectDataStep>(result);
        }

        [Fact]
        public void FlowTemplateStepDto_should_map_to_StoreDataStep_when_mapped_to_IStep()
        {
            var origin = new FlowTemplateStepDto { Name = "StoreDataStep Example", StepTypeName = "StoreDataStep" };

            var result = Mapper.Map<IStep>(origin);

            Assert.IsType<StoreDataStep>(result);
        }
    }
}
