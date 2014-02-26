using System.Collections.Generic;
using System.Linq;
using Flow.Library.Steps;

namespace Flow.Library.Core
{
    public class FlowTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StepBase> Steps { get; set; }
        public List<StepBase> RequiredSteps { get; set; }
        public List<Link> Nodes { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public StepBase StartNode { get { return Steps.First(); } }
    
        public FlowTemplate()
        {
            Id = 0;
            Name = string.Empty;
            Steps = new List<StepBase>();
            RequiredSteps = new List<StepBase>();
            Nodes = new List<Link>();
            Variables = new Dictionary<string, object>();
        }
    }
}
