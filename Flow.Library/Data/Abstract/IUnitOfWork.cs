using Flow.Library.Steps;
using Flow.Library.Validation;

namespace Flow.Library.Data.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<Core.FlowTemplate> FlowTemplates { get; set; }

        IRepository<IFlowTemplateStep> FlowTemplateSteps { get; set; }

        IRepository<IValidationRule> FlowTemplateStepRules { get; set; }

        void Commit();
    }
}
