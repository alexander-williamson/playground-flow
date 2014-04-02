using System.Collections.Generic;
using Flow.Library.Steps;

namespace Flow.Library.Core
{
    public class FlowTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IStep> Steps { get; set; }
        public List<Link> Nodes { get; set; }
        public Dictionary<string, object> Variables { get; set; }
    }

}