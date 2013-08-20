using System.Collections.Generic;
using System.Linq;
using Flow.Library.Steps;

namespace Flow.Library
{
    public class FlowInstance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StepBase> RequiredSteps { get; set; }
        public List<StepBase> Steps { get; set; }
        public List<Link> Nodes { get; set; }
        public FlowState CurrentState { get; private set; }
        public Dictionary<string, object> Variables { get; set; }

        public FlowInstance()
        {
            Id = 1;
            Name = "Collect Data Template";
            Variables = new Dictionary<string, object>();
            Steps = new List<StepBase>();
            Nodes = new List<Link>();
            Variables = new Dictionary<string, object>();
            CurrentState = FlowState.New;
        }

        public List<StepBase> CompletedSteps()
        {
            return (from o in Steps where o.IsComplete select o).OrderBy(o => o.Id).ToList(); ;
        }

        public List<StepBase> NextSteps()
        {
            // TODO: make this walk through all the steps in any possible node list
            // note this is a basic implementation that just gets the next item (no branching)
            var result = new List<StepBase>();
            var items = (from o in Steps where o.IsComplete == false select o).OrderBy(o => o.Id);

            if (items.Count() > 1)
                result.Add(items.First());

            return result;
        }
    }
}
