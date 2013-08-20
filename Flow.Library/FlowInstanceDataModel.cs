namespace Flow.Library
{
    public class FlowInstanceDataModel
    {
        public int Id { get; set; }
        public int FlowTemplateId { get; set; }
        public string Name { get; set; }
    }

    public class FlowInstanceStep
    {
        public int Id { get; set; }
        public int FlowInstanceId { get; set; }
        public string Name { get; set; }
    }

    public class FlowInstanceVariableDataModel
    {
        public int Id { get; set; }
        public int FlowInstanceId { get; set; }
        public int Name { get; set; }
        public string Value { get; set; }
    }

    public class FlowTemplateDataModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }

    public class FlowTemplateStepDataModel
    {
        public int Id { get; set; }
        public int FlowTemplateId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}