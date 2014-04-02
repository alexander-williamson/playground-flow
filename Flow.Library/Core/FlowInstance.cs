using System.Collections.Generic;
using System.Linq;
using Flow.Library.Steps;

namespace Flow.Library.Core
{
    public class FlowInstance : ITrackDirty
    {
        public int Id { get; set; }
        public FlowTemplate Template { get; set; }
        public IDictionary<string, object> Variables { get; set; }
        public List<CompletedStep> CompletedSteps { get; set; }

        public FlowInstance()
        {
            CompletedSteps = new List<CompletedStep>();
            Variables = new Dictionary<string, object>();
        }

        public IRunnableStep NextStep()
        {
            var ids = (from o in CompletedSteps select o.StepId);
            var notComplete = (from o in Template.Steps where !ids.Contains(o.Id) select (IRunnableStep)o).ToList();
            return notComplete.Any() ? notComplete.First() : null;
        }

        public bool IsDirty { get; set; }
    }
}