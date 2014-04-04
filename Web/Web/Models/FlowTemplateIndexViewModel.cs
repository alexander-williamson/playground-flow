using System.Collections.Generic;
using Flow.Library.Core;

namespace Web.Models
{
    public class FlowTemplateIndexViewModel
    {
        public IEnumerable<FlowTemplate> Templates { get; set; }
    }

    public class FlowTemplateAddModel
    {
        public string Name { get; set; }
    }
}