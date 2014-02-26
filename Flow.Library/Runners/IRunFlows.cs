using System;
using Flow.Library.Actions;

namespace Flow.Library.Runners
{
    public interface IRunFlows
    {
        ActionBase ProcessSteps();
        string Name { get; }
        bool CanProcess(Type type);
    }
}