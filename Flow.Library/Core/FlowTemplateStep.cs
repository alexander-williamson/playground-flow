using System.Collections.Generic;
using Flow.Library.Data.Abstract;
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
    }
}