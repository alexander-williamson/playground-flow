using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Data.Repositories
{
    public class FlowTemplateSqlRepository : IFlowTemplateRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public FlowTemplateSqlRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public IEnumerable<FlowTemplate> Get()
        {
            if(_connection.State == ConnectionState.Closed) 
                _connection.Open();

            return _connection.Query<FlowTemplate>("SELECT * FROM FlowTemplate", null, _transaction);
        }

        public FlowTemplate Get(int id)
        {
            if (_connection.State == ConnectionState.Closed) 
                _connection.Open();

            var result = _connection.Query<FlowTemplate>("SELECT * FROM FlowTemplate WHERE Id = @Id", new { Id = id }, _transaction).ToArray();
            return result.First();
        }

        public int Add(FlowTemplate instance)
        {
            if(_connection.State == ConnectionState.Closed) 
                _connection.Open();

            var id = _connection.Query<int>("SELECT TOP 1 Id FROM FlowTemplate ORDER BY Id DESC", null, _transaction).First();
            instance.Id = ++id;
            _connection.Execute("INSERT INTO FlowTemplate (Id, Name) VALUES (@id, @name)", instance, _transaction);
            return _connection.Query<int>("SELECT TOP 1 Id FROM FlowTemplate ORDER BY Id DESC", null, _transaction).First();

        }

        public bool Update(int id, FlowTemplate instance)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
