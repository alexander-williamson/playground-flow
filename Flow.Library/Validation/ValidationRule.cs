namespace Flow.Library.Validation
{
    public class ValidationRule
    {
        public string Key { get; set; }
        public IRule Validator { get; set; }
    }
}