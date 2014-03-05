using System.Collections.Generic;
using System.Linq;
using Flow.Library.Steps;

namespace Flow.Library.Core
{
    public class FlowInstance
    {
        public int Id { get; set; }
        public FlowTemplate Template { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public List<CompletedStep> CompletedSteps { get; set; }

        public FlowInstance()
        {
            CompletedSteps = new List<CompletedStep>();
            Variables = new Dictionary<string, object>();
        }

        public IStep NextStep()
        {
            var ids = (from o in CompletedSteps select o.StepId);
            var notComplete = (from o in Template.Steps where !ids.Contains(o.Id) select o).ToList();
            return notComplete.Any() ? notComplete.First() : null;
        }
    }
}