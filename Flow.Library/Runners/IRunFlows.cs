using System;
using Flow.Library.Actions;

namespace Flow.Library.Runners
{
    public interface IRunFlows
    {
        IAction ProcessSteps();
        string Name { get; }
        bool CanProcess(Type type);
    }
}