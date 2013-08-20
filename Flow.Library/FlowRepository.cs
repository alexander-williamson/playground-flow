using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;   

namespace Flow.Library
{
    public interface IFlowRepository
    {
        FlowInstance GetFlowInstance(int id);
    }

    public class FlowRepository : IFlowRepository
    {
        public FlowInstance GetFlowInstance(int id)
        {
            const string connectionString = "Data Source=.;Initial Catalog=Flow;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
               return connection.Query<FlowInstance>("SELECT * FROM FlowInstance WHERE Id = @Id", new {Id = id}).First();
            }
        }
    }

}
