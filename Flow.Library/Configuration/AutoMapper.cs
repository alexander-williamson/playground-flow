using AutoMapper;
using Flow.Library.Core;
using Flow.Library.Steps;

namespace Flow.Library.Configuration
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<StartStep, FlowTemplateStep>().ForMember(o => o.StepTypeId, o => o.UseValue(1));
            Mapper.CreateMap<StopStep, FlowTemplateStep>().ForMember(o => o.StepTypeId, o => o.UseValue(2));
            Mapper.CreateMap<CollectDataStep, FlowTemplateStep>().ForMember(o => o.StepTypeId, o => o.UseValue(3));
            Mapper.CreateMap<StoreDataStep, FlowTemplateStep>().ForMember(o => o.StepTypeId, o => o.UseValue(4));
        }
    }
}
