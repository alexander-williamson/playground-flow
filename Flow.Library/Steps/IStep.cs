using System.Collections.Generic;
using Flow.Library.Core;
using Flow.Library.Validation;

namespace Flow.Library.Steps
{

    public interface IStep : ITrackDirty
    {
        int Id { get; set; }
        int VersionId { get; set; }
        string Name { get; set; }
        IEnumerable<IValidationRule> EntryRules { get; set; }
        IEnumerable<IValidationRule> ExitRules { get; set; }
    }
}