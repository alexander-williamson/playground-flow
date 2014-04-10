using System;
using AutoMapper;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
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
            Mapper.CreateMap<IFlowTemplateStep, FlowTemplateStepDto>()
                .ForMember(o => o.StepTypeName, d => d.MapFrom(o => GetStepTypeName(o.StepTypeId)));

            Mapper.CreateMap<FlowTemplateStepRule, ValidationRuleDto>();

            Mapper.CreateMap<FlowTemplateDto, FlowTemplate>();

            Mapper.CreateMap<FlowTemplateStepDto, StartStep>();
            Mapper.CreateMap<FlowTemplateStepDto, StopStep>();
            Mapper.CreateMap<FlowTemplateStepDto, CollectDataStep>();
            Mapper.CreateMap<FlowTemplateStepDto, StoreDataStep>();

            Mapper.CreateMap<StartStep, FlowTemplateStepDto>().ForMember(o => o.StepTypeName, v=> v.UseValue("StartStep"));
            Mapper.CreateMap<StopStep, FlowTemplateStepDto>().ForMember(o => o.StepTypeName, v => v.UseValue("StopStep")); ;
            Mapper.CreateMap<CollectDataStep, FlowTemplateStepDto>().ForMember(o => o.StepTypeName, v => v.UseValue("CollectDataStep")); ;
            Mapper.CreateMap<StoreDataStep, FlowTemplateStepDto>().ForMember(o => o.StepTypeName, v => v.UseValue("StoreDataStep"));
        
            Mapper.CreateMap<FlowTemplateStepDto, IStep>().ConvertUsing(FlowTemplateDtoToIStep);
        }

        private static IStep FlowTemplateDtoToIStep(FlowTemplateStepDto arg)
        {
            switch (arg.StepTypeName)
            {
                case "StartStep":
                    return Mapper.Map<StartStep>(arg);
                case "StopStep":
                    return Mapper.Map<StopStep>(arg);
                case "CollectDataStep":
                    return Mapper.Map<CollectDataStep>(arg);
                case "StoreDataStep":
                    return Mapper.Map<StoreDataStep>(arg);
                default:
                    throw new NotSupportedException("Unable to map from " + arg.GetType().Name + " to a supported step");
            }
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