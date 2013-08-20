using System;
using System.Collections.Generic;
using Flow.Library;

namespace Flow.Library.Validation
{
    public class RuleSet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ValidationRule> Rules { get; set; }

        public RuleSet()
        {
            Rules = new List<ValidationRule>();
        }
    }
}