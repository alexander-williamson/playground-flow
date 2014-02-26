using System;
using System.Collections.Generic;
using System.Linq;
using Flow.Library.Actions;
using Flow.Library.Core;
using Flow.Library.Steps;

namespace Flow.Library.Runners
{
    // This class is a runner for a flow instance
    // When the process next step is called, it looks at the flow instance, decides if it can run the next step
    public class FlowRunner : IRunFlows
    {
        protected FlowInstance FlowInstance;
        protected List<Type> Types = new[] { typeof(StepBase), typeof(StartStep), typeof(StopStep) }.ToList();

        public FlowRunner(FlowInstance instance)
        {
            FlowInstance = instance;
        }

        // process the steps until we get a NoAction
        // for example if we needed to show the form to someone, this base handler doesn't understand that
        // so this would have to be picked up by a different type of runner
        // for example a webapi handler would be able to process a collect data action
        public virtual ActionBase ProcessSteps()
        {
            var stepInstance = FlowInstance.NextStep();
            while(stepInstance != null)
            {
                if (!CanProcess(stepInstance.GetType()))
                    return new UnhandlableAction { Step = stepInstance };

                stepInstance.Process(FlowInstance, this);
                stepInstance = FlowInstance.NextStep();
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