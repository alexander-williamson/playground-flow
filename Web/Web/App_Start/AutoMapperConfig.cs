using AutoMapper;
using Flow.Library.Core;

namespace Web
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<FlowTemplate, Dto.FlowTemplate>();
            Mapper.CreateMap<FlowTemplateStep, Dto.FlowTemplateStep>();
            Mapper.CreateMap<FlowTemplateStepRule, Dto.ValidationRule>();

            Mapper.CreateMap<Dto.FlowTemplate, FlowTemplate>();
        }
    }
}