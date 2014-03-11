using System.Data;
using System.Linq;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
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

        public FlowInstance Get(int id)
        {
            var flow =  _dbConnection.Query<FlowInstance>("SELECT TOP 1 * FROM FlowInstance", new { Id = id }, _transaction).First();
            return flow;
        }

        public int Add(FlowInstance instance)
        {
            var id = _dbConnection.Query<int>("SELECT TOP 1 Id FROM FlowInstance ORDER BY Id DESC", null, _transaction).First();
            id++;
            _dbConnection.Execute("INSERT INTO FlowInstance (Id) VALUES (@Id)", new { Id = id }, _transaction);
            return _dbConnection.Query<int>("SELECT TOP 1 Id FROM FlowInstance ORDER BY FlowInstance.Id DESC", null, _transaction).First();
        }
    }
}
