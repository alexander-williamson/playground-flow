using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Data.Repositories
{
    public class FlowInstanceSqlRepository : IFlowInstanceRepository
    {
        private const string ConnectionString = "Data Source=.;Initial Catalog=Flow;Integrated Security=True";

        public FlowInstance GetFlow(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowInstance>("SELECT * FROM FlowInstance WHERE FlowInstance = @Id", new { Id = id }).First();
            }
        }
    }

}
