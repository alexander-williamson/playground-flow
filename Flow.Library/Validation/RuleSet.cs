using System;
using System.Collections.Generic;

namespace Flow.Library.Validation
{
    public class RuleSet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<IValidationRule> Rules { get; set; }

        public RuleSet()
        {
            Rules = new List<IValidationRule>();
        }
    }
}