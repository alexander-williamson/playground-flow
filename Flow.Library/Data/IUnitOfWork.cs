using Flow.Library.Core;

namespace Flow.Library.Data
{
    public interface IUnitOfWork
    {
        IRepository<FlowTemplate> FlowInstances { get; }
        IRepository<FlowTemplateStep> FlowTemplateSteps { get; }
        IRepository<FlowTemplateStepRule> FlowTemplateStepRules { get; }
        void Commit();
    }
}