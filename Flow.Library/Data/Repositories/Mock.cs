using System.Data;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Library.Validation.Rules;
using System.Collections.Generic;

namespace Flow.Library.Data.Repositories
{
    public class MockFlowInstanceRepository : IFlowInstanceRepository
    {
        public FlowInstance Get(int id, IDbTransaction transaction)
        {
            var template = new FlowTemplate();
            template.Variables.Add("yourName", string.Empty);
            template.Steps.Add(new DataCollectionStep
            {
                ExitRules = new List<IValidationRule> { new StringRequired { VariableKey = "yourName" } }
            });
            template.Steps.Add(new StoreDataStep());
            return new FlowInstance(); ;
        }

        public int Add(FlowInstance instance, IDbTransaction transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}
