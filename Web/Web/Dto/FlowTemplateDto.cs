using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flow.Web.Dto
{
    public class FlowTemplateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(1)]
        public string Name { get; set; }
        public List<FlowTemplateStepDto> Steps { get; set; }
        public Dictionary<string, object> Variables { get; set; }
    }

    public class FlowTemplateStepDto
    {
        public int Id { get; set; }
        public int VersionId { get; set; }
        public string StepTypeName { get; set; }
        public string Name { get; set; }
        public List<ValidationRuleDto> EntryRules { get; set; }
        public List<ValidationRuleDto> ExitRules { get; set; }
    }

    public class ValidationRuleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

}