using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Dto
{
    public class FlowTemplate
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(1)]
        public string Name { get; set; }
        public List<FlowTemplateStep> Steps { get; set; }
        public Dictionary<string, object> Variables { get; set; }
    }

    public class FlowTemplateStep
    {
        public int Id { get; set; }
        public int VersionId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public List<ValidationRule> EntryRules { get; set; }
        public List<ValidationRule> ExitRules { get; set; }
    }

    public class ValidationRule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

}