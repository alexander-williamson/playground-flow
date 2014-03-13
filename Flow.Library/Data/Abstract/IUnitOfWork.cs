using Flow.Library.Core;

namespace Flow.Library.Data.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<Core.FlowTemplate> FlowTemplates { get; set; }

        IRepository<FlowTemplateStep> FlowTemplateSteps { get; set; }

        IRepository<FlowTemplateStepRule> FlowTemplateStepRules { get; set; }

        void Commit();
    }
}
