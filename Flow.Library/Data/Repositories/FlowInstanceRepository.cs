using System.Collections.Generic;
using System.Linq;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;

namespace Flow.Library.Data.Repositories
{
    public class FlowInstanceRepository : IFlowInstanceRepository
    {
        private readonly IDataContext _dataContext;

        public FlowInstanceRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public FlowInstance GetFlow(int id)
        {
            var flow =  _dataContext.Query<FlowInstance>("SELECT * FROM FlowInstance WHERE FlowInstance = @Id", new { Id = id }).First();
            
            var vars = _dataContext.Query<KeyValuePair<string, object>>("SELECT Key, Value FROM FlowInstanceVariable WHERE FlowInstanceId = @Id", new { Id = id });
            flow.Variables = vars.ToDictionary(o =>o.Key, o=> o.Value);

            var steps = _dataContext.Query<CompletedStep>("SELECT * FROM CompletedStep WHERE FlowInstance = @Id", new { Id = id });
            flow.CompletedSteps = steps.ToList();

            return flow;
        }
    }

}
