using System.Collections.Generic;
using System.Linq;
using Flow.Library.Steps;

namespace Flow.Library.Core
{
    public class CompletedStep
    {
        public int StepId { get; set; }
        public int StepVersion { get; set; } 
    }
    
    public class FlowInstance
    {
        public int Id { get; set; }
        public FlowTemplate Template { get; set; }
        public FlowState CurrentState { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public List<StepBase> Steps { get; set; }

        public FlowInstance(FlowTemplate template)
        {
            Template = template;
            Steps = template.Steps.ToList();
            Variables = template.Variables.ToDictionary(k => k.Key, v => v.Value);
        }

        // Resume the worflow
        // Inform all the steps if they've been processed before or not, because they don't know
        // Ideally the steps will know if they have been processed, but they've just been loaded from the database
        //  so they have NO idea what's going on, poor chaps
        public void Resume(List<CompletedStep> completedStepVersions)
        {
            foreach(var step in Steps)
            {
                step.IsInitialized = true;
                
                var instance = step;
                var matches = (from o in completedStepVersions where o.StepId == instance.Id && o.StepVersion == instance.VersionId select o).ToList();
                if (matches.Any())
                    step.IsProcessed = true;
            }
        }

        public List<StepBase> CompletedSteps()
        {
            return (from o in Steps where o.IsComplete select o).OrderBy(o => o.Id).ToList();
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