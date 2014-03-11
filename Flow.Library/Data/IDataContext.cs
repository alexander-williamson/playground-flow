using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Flow.Library.Data
{
    public interface IDataContext
    {
        IEnumerable<T> Query<T>(string query, object param);
    }

    public class DapperDataContext : IDataContext
    {
        private readonly IDbConnection _dbConnection;

        public DapperDataContext(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<T> Query<T>(string query, object param)
        {
            return _dbConnection.Query<T>(query, param);
        }
    }

}
