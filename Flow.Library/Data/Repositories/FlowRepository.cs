using Flow.Library.Core;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Library.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow.Library.Data.Repositories
{
    public class FlowRepository
    {
        public static FlowInstance GetFlow(int FlowId, int InstanceId)
        {
            var template = new FlowTemplate();
            template.Variables.Add("yourName", string.Empty);
            template.Steps.Add(new DataCollectionStep
            {
                ExitRules = new List<ValidationRule> { new ValidationRule { Key = "yourName", Validator = new StringRequired() } }
            });
            template.Steps.Add(new StoreDataStep());
            //template.Steps.Add(new ShowInformationPage { Page = "Example" });
            return new FlowInstance(template);;
        }
    }
}
