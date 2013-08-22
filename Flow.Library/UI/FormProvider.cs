using System.Collections.Generic;
using Flow.Library.Validation;

namespace Flow.Library
{
    public interface IFormProvider
    {
        Dictionary<string, object> Arguments { get; set; }
        Dictionary<string, object> Variables { get; set; }
        bool HasData { get; set; }
    }

    public class FormProvider : IFormProvider
    {
        // initialisation arguments
        public Dictionary<string, object> Arguments { get; set; }

        // live varialbles
        public Dictionary<string, object> Variables { get; set; }

        // true when the form has data
        public bool HasData { get; set; }

        public List<BrokenRule> BrokenRules
        {
            get { throw new System.NotImplementedException(); }
        }

        public FormProvider(Dictionary<string, object> arguments = null)
        {
            Arguments = arguments ?? new Dictionary<string, object>();
            Variables = new Dictionary<string, object>(Arguments);
            HasData = true;
        }
    }
}
