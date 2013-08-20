using Flow.Library;

namespace Flow.Library
{
    public class ValidationRule
    {
        public string Key { get; set; }
        public IValidate Validator { get; set; }
    }
}