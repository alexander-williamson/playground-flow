using System;
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
            Mapper.CreateMap<FlowTemplateStepRule, ValidationRuleDto>();

            Mapper.CreateMap<FlowTemplateDto, FlowTemplate>();

            Mapper.CreateMap<FlowTemplateStepDto, StartStep>();
            Mapper.CreateMap<FlowTemplateStepDto, StopStep>();
            Mapper.CreateMap<FlowTemplateStepDto, CollectDataStep>();
            Mapper.CreateMap<FlowTemplateStepDto, StoreDataStep>();

            Mapper.CreateMap<StartStep, FlowTemplateStepDto>().ForMember(destination => destination.StepTypeName, v=> v.UseValue("StartStep"));
            Mapper.CreateMap<StopStep, FlowTemplateStepDto>().ForMember(destination => destination.StepTypeName, v => v.UseValue("StopStep")); ;
            Mapper.CreateMap<CollectDataStep, FlowTemplateStepDto>().ForMember(destination => destination.StepTypeName, v => v.UseValue("CollectDataStep")); ;
            Mapper.CreateMap<StoreDataStep, FlowTemplateStepDto>().ForMember(destination => destination.StepTypeName, v => v.UseValue("StoreDataStep"));
        
            Mapper.CreateMap<FlowTemplateStepDto, IStep>().ConvertUsing(FlowTemplateDtoToIStep);
        }

        private static IStep FlowTemplateDtoToIStep(FlowTemplateStepDto source)
        {
            switch (source.StepTypeName)
            {
                case "StartStep":
                    return Mapper.Map<StartStep>(source);
                case "StopStep":
                    return Mapper.Map<StopStep>(source);
                case "CollectDataStep":
                    return Mapper.Map<CollectDataStep>(source);
                case "StoreDataStep":
                    return Mapper.Map<StoreDataStep>(source);
                default:
                    throw new NotSupportedException("Unable to map from " + source.GetType().Name + " to a supported step");
            }
        }
    }
}