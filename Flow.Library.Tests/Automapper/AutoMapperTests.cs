using AutoMapper;
using Flow.Library.Configuration;
using Flow.Library.Core;
using Flow.Library.Steps;
using Xunit;

namespace Flow.Library.Tests.Automapper
{
    public class AutoMapperTests
    {
        public AutoMapperTests()
        {
            AutoMapperConfig.Configure();
           
        }

        [Fact]
        public void Should_map_StartStep_value_types_to_FlowTemplateStep()
        {
            var step = new StartStep
            {
                Id = 10,
                Name = "Start Step 1"
            };

            // Act
            var result = Mapper.Map<FlowTemplateStep>(step);

            // Assert
            Assert.IsType<FlowTemplateStep>(result);
            Assert.Equal(10, result.Id);
            Assert.Equal("Start Step 1", result.Name);
            Assert.Equal(1, result.StepTypeId);
        }

        [Fact]
        public void Should_map_StopStep_value_typesto_FlowTemplateStep()
        {
            var step = new StopStep
            {
                Id = 22,
                Name = "Stop Step 2"
            };

            // Act
            var result = Mapper.Map<FlowTemplateStep>(step);

            // Assert
            Assert.IsType<FlowTemplateStep>(result);
            Assert.Equal(22, result.Id);
            Assert.Equal("Stop Step 2", result.Name);
            Assert.Equal(2, result.StepTypeId);
        }

        [Fact]
        public void Should_map_CollectDataStep_value_typesto_FlowTemplateStep()
        {
            var step = new CollectDataStep()
            {
                Id = 33,
                Name = "Example CollectData Step 3"
            };

            // Act
            var result = Mapper.Map<FlowTemplateStep>(step);

            // Assert
            Assert.IsType<FlowTemplateStep>(result);
            Assert.Equal(33, result.Id);
            Assert.Equal("Example CollectData Step 3", result.Name);
            Assert.Equal(3, result.StepTypeId);
        }

        [Fact]
        public void Should_map_StoreStep_value_types_to_FlowTemplateStep()
        {
            var step = new StoreDataStep()
            {
                Id = 14,
                Name = "Store Data 4"
            };

            // Act
            var result = Mapper.Map<FlowTemplateStep>(step);

            // Assert
            Assert.IsType<FlowTemplateStep>(result);
            Assert.Equal(14, result.Id);
            Assert.Equal("Store Data 4", result.Name);
            Assert.Equal(4, result.StepTypeId);
        }

        [Fact]
        public void Should_map_StartStep_even_if_variable_is_abstract()
        {
            IStep step = new StartStep();

            var result = Mapper.Map<FlowTemplateStep>(step);

            Assert.IsType<FlowTemplateStep>(result);
        }

    }
}
