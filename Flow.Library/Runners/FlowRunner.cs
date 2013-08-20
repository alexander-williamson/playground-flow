using System;
using System.Collections.Generic;
using System.Linq;
using Flow.Library.Actions;
using Flow.Library.Steps;

namespace Flow.Library.Runners
{
    // This class is a runner for a flow instance
    // When the process next step is called, it looks at the flow instance, decides if it can run the next step
    public class FlowRunner : IRunFlows
    {
        protected FlowInstance FlowInstance;
        protected List<Type> Types = new[] { typeof(StepBase), typeof(StartStepBase), typeof(StopStepBase) }.ToList();

        public FlowRunner(FlowInstance instance)
        {
            FlowInstance = instance;
        }

        // process the steps until we get a NoAction
        // for example if we needed to show the form to someone, this base handler doesn't understand that
        // so this would have to be picked up by a different type of runner
        // for example a webapi handler would be able to process a collect data action
        public virtual IAction ProcessSteps()
        {
            var steps = (from o in FlowInstance.Steps where o.IsComplete == false select o).ToList();
            while (steps.Any())
            {
                // a flow instance would have just been loaded from the database
                // so it would know it's current state
                steps = (from o in FlowInstance.Steps where o.IsComplete == false select o).ToList();

                var step = steps.First();
                if (!CanProcess(step.GetType()))
                    return new UnhandlableAction { Step = step };

                step.Process(FlowInstance, this);
            }
            return new NoAction();
        }

        public virtual string Name
        {
            get { return "Default Flow Runner"; }
        }

        public bool CanProcess(Type type)
        {
            return Types.Contains(type);
        }
    }
}