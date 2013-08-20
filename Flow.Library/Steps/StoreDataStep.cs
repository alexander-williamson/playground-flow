using System;
using System.IO;
using Flow.Library.Runners;

namespace Flow.Library.Steps
{
    public class StoreDataStep : StepBase
    {
        public StoreDataStep(FlowInstance flow) : base(flow)
        {
            // do nothing
            
        }
        public override void Process(FlowInstance flow, IRunFlows runner)
        {
            const string path = @"C:\Temp\output.txt";

            if(File.Exists(path))
                File.Delete(path);

            foreach(var element in flow.Variables)
            {
                var line = string.Format("{0}: {1}{2}", element.Key, element.Value, Environment.NewLine);
                File.AppendAllText(path, line);
            }
            base.Process(flow, runner);
        }
    }
}