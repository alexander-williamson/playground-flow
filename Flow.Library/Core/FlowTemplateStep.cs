using System.Collections.Generic;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Repositories;
using Flow.Library.Steps;
using Flow.Library.Validation;

namespace Flow.Library.Core
{
    public class FlowTemplateStep : IFlowTemplateStep
    {
        public int Id { get; set; }
        public int VersionId { get; set; }
        public string Name { get; set; }
        public List<IValidationRule> EntryRules { get; set; }
        public List<IValidationRule> ExitRules { get; set; }
        public bool IsDirty { get; set; }
        public int FlowTemplateId { get; set; }

        public FlowTemplateStep(IStep step)
        {
            EntryRules = step.EntryRules;
            ExitRules = step.ExitRules;
            Id = step.Id;
            IsDirty = step.IsDirty;
            Name = step.Name;
            VersionId = step.Id;
        }

        public FlowTemplateStep()
        {
            // default constructor
        }
    }
}