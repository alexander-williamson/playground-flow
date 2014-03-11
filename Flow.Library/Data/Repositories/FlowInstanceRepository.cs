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

        public FlowInstanceRepository(IDbConnection dbConnection, IDbTransaction transaction)
        {
            _dbConnection = dbConnection;
        }

        public FlowInstance Get(int id, IDbTransaction transaction = null)
        {
            var flow = _dbConnection.Query<FlowInstance>("SELECT TOP 1 * FROM FlowInstance WHERE Id=@id ", new { id }, transaction).First();
            return flow;
        }

        public int Add(FlowInstance instance, IDbTransaction transaction = null)
        {
            var id = _dbConnection.Query<int>("SELECT TOP 1 Id FROM FlowInstance ORDER BY Id DESC", null, transaction).First();
            id++;
            _dbConnection.Execute("INSERT INTO FlowInstance (Id) VALUES (@id)", new { id }, transaction);
            return _dbConnection.Query<int>("SELECT TOP 1 Id FROM FlowInstance ORDER BY FlowInstance.Id DESC", null, transaction).First();
        }
    }
}
