namespace Flow.Library.Data.Abstract
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        public IRepository<Core.FlowTemplate> FlowTemplates { get; set; }

        public IRepository<Core.FlowTemplateStep> FlowTemplateSteps { get; set; }

        public IRepository<Core.FlowTemplateStepRule> FlowTemplateStepRules { get; set; }

        public void Commit()
        {

        }
    }
}