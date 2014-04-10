using System.Collections.Generic;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;

namespace Flow.Library.Core
{
    public class FlowTemplateStep : IFlowTemplateStep, IStep
    {
        public int Id { get; set; }
        public int VersionId { get; set; }
        public string Name { get; set; }
        public IEnumerable<IValidationRule> EntryRules { get; set; }
        public IEnumerable<IValidationRule> ExitRules { get; set; }
        public bool IsDirty { get; set; }
        public int FlowTemplateId { get; set; }
        public int StepTypeId { get; set; }
    }
}