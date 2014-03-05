using System.Collections.Generic;
using System.Linq;
using Flow.Library.Steps;

namespace Flow.Library.Core
{
    public class FlowTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IStep> Steps { get; set; }
        public List<IStep> RequiredSteps { get; set; }
        public List<Link> Nodes { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public IStep StartNode { get { return Steps.First(); } }
    
        public FlowTemplate()
        {
            Id = 0;
            Name = string.Empty;
            Steps = new List<IStep>();
            RequiredSteps = new List<IStep>();
            Nodes = new List<Link>();
            Variables = new Dictionary<string, object>();
        }
    }
}
