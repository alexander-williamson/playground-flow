namespace Flow.Library.Data.Models
{
    public class FlowTemplateStepDataModel
    {
        public int Id { get; set; }
        public int FlowTemplateId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}