using System.Collections.Generic;
using System.Linq;
using Flow.Library.Steps;

namespace Flow.Library.Core
{
    public class FlowInstance
    {
        private readonly List<CompletedStep> _completedSteps; 
        
        public int Id { get; set; }
        public FlowTemplate Template { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public List<CompletedStep> CompletedSteps { get { return _completedSteps;  } }

        public FlowInstance()
        {
            _completedSteps = new List<CompletedStep>();
        }

        public StepBase NextStep()
        {
            var ids = (from o in CompletedSteps select o.StepId);
            var notComplete = (from o in Template.Steps where !ids.Contains(o.Id) select o).ToList();
            return notComplete.Any() ? notComplete.First() : null;
        }
    }
}