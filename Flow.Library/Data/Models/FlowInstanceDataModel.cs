using System.Collections.Generic;

namespace Flow.Library.Data.Models
{
    public class FlowInstanceStepDataModel
    {
        public int Id { get; set; }
        public int FlowInstanceId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
    public class FlowInstanceDataModel
    {
        public int Id { get; set; }
        public int FlowTemplateId { get; set; }
        public IEnumerable<FlowInstanceStepDataModel> Steps { get; set; }
        public IDictionary<string, object> Variables { get; set; }
    }
}