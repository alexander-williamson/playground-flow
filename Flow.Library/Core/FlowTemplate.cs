using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Flow.Library.Steps;

namespace Flow.Library.Core
{
    public class FlowTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StepBase> Steps { get; set; }
        public IEnumerable<StepBase> RequiredSteps { get; set; }
        public IEnumerable<Link> Nodes { get; set; }
        public IDictionary<string, object> Variables { get; set; }
        public StepBase StartNode { get { return Steps.First(); } }
    
        public FlowTemplate()
        {
            Id = 0;
            Name = string.Empty;
            Steps = new List<StepBase>();
            RequiredSteps = new List<StepBase>();
            Nodes = new BindingList<Link>();
            Variables = new Dictionary<string, object>();
        }
    }
}
