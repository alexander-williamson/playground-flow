using System.Collections.Generic;
using System.Data;
using System.Linq;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;
using Dapper;

namespace Flow.Library.Data.Repositories
{
    public class FlowInstanceRepository : IFlowInstanceRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _transaction;

        public FlowInstanceRepository(IDbConnection dbConnection, IDbTransaction transaction)
        {
            _dbConnection = dbConnection;
            _transaction = transaction;
        }

        public FlowInstance GetFlow(int id)
        {
            var flow =  _dbConnection.Query<FlowInstance>("SELECT * FROM FlowInstance WHERE Id = @Id", new { Id = id }, _transaction).First();
            return flow;
        }
    }

}
