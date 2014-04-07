using System;
using AutoMapper;
using Flow.Library.Core;
using Flow.Library.Steps;
using Web.Dto;

namespace Web
{
    public class AutoMapperConfig
    {
        // TODO write tests for mapping
        public static void Configure()
        {
            Mapper.CreateMap<FlowTemplate, FlowTemplateDto>();
            Mapper.CreateMap<FlowTemplateStep, FlowTemplateStepDto>()
                .ForMember(o => o.Type, d => d.MapFrom(o => GetTypeName(o.StepTypeId)));

            Mapper.CreateMap<FlowTemplateStepRule, ValidationRuleDto>();

            Mapper.CreateMap<FlowTemplateDto, FlowTemplate>();
            Mapper.CreateMap<FlowTemplateStepDto, FlowTemplateStep>();

            Mapper.CreateMap<FlowTemplateStepDto, StartStep>();
            Mapper.CreateMap<FlowTemplateStepDto, StopStep>();
            Mapper.CreateMap<FlowTemplateStepDto, CollectDataStep>();
            Mapper.CreateMap<FlowTemplateStepDto, StoreDataStep>();
        }

        // TODO move to better location
        public static string GetTypeName(int id)
        {
            switch (id)
            {
                case 1:
                    return "StartStep";
                case 2:
                    return "StopStep";
                case 3:
                    return "CollectDataStep";
                case 4:
                    return "StoreDataStep";
                default:
                    throw new NotSupportedException("Unknown id");
            }
        }
    }
}