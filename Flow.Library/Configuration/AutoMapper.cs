using System;
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

            Mapper.CreateMap<FlowTemplateStep, StartStep>();
            Mapper.CreateMap<FlowTemplateStep, StopStep>();
            Mapper.CreateMap<FlowTemplateStep, CollectDataStep>();
            Mapper.CreateMap<FlowTemplateStep, StoreDataStep>();

            Mapper.CreateMap<FlowTemplateStep, IStep>().ConvertUsing(source =>
            {
                switch (source.StepTypeId)
                {
                    case 1:
                        return Mapper.Map<StartStep>(source);
                    case 2:
                        return Mapper.Map<StopStep>(source);
                    case 3:
                        return Mapper.Map<CollectDataStep>(source);
                    case 4:
                        return Mapper.Map<StoreDataStep>(source);
                }
                throw new NotSupportedException();
            });
        }
    }
}
