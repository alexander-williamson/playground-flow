using System.Collections.Generic;

namespace Flow.Library.Core
{
    public class FlowTemplateStep
    {
        public int Id { get; set; }
        public List<FlowTemplateStepRule> Rules { get; set; }
    }
}