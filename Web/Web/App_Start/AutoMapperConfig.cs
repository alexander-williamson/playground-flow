using System;
using System.CodeDom;
using AutoMapper;
using Flow.Library.Core;
using Flow.Library.Steps;
using Flow.Web.Dto;

namespace Flow.Web
{
    public class AutoMapperConfig
    {
        // TODO write tests for mapping
        public static void Configure()
        {
            Mapper.CreateMap<FlowTemplate, FlowTemplateDto>();
            Mapper.CreateMap<FlowTemplateStep, FlowTemplateStepDto>()
                .ForMember(o => o.StepTypeName, d => d.MapFrom(o => GetStepTypeName(o.StepTypeId)));

            Mapper.CreateMap<FlowTemplateStepRule, ValidationRuleDto>();

            Mapper.CreateMap<FlowTemplateDto, FlowTemplate>();
            Mapper.CreateMap<FlowTemplateStepDto, FlowTemplateStep>();

            Mapper.CreateMap<FlowTemplateStepDto, StartStep>();
            Mapper.CreateMap<FlowTemplateStepDto, StopStep>();
            Mapper.CreateMap<FlowTemplateStepDto, CollectDataStep>();
            Mapper.CreateMap<FlowTemplateStepDto, StoreDataStep>();
        }

        // TODO move to better location
        public static string GetStepTypeName(int id)
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
                    throw new NotSupportedException("Unable to map to StepTypeName. Unknown id.");
            }
        }
    }
}