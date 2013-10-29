using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Models;

namespace Flow.Library.Data.Repositories
{
    public class FlowInstanceSqlRepository : IFlowInstanceRepository
    {
        private const string ConnectionString = "Data Source=.;Initial Catalog=Flow;Integrated Security=True";

        public FlowInstanceDataModel GetFlow(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowInstanceDataModel>("SELECT * FROM FlowInstance WHERE FlowInstance = @Id", new { Id = id }).First();
            }
        }
    }

}
