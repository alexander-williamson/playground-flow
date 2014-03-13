//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using Dapper;
//using Flow.Library.Core;
//using Flow.Library.Data.Abstract;

//namespace Flow.Library.Data.Repositories
//{
//    public class FlowTemplateSqlRepository : IFlowTemplateRepository
//    {
//        private readonly IDbConnection _connection;

//        public FlowTemplateSqlRepository(IDbConnection connection)
//        {
//            _connection = connection;
//        }

//        public IEnumerable<Core.FlowTemplate> Get(IDbTransaction transaction = null)
//        {
//            if(_connection.State == ConnectionState.Closed) 
//                _connection.Open();

//            return _connection.Query<Core.FlowTemplate>("SELECT * FROM FlowTemplate", null, transaction);
//        }

//        public Core.FlowTemplate Get(int id, IDbTransaction transaction = null)
//        {
//            if (_connection.State == ConnectionState.Closed) 
//                _connection.Open();

//            var result = _connection.Query<Core.FlowTemplate>("SELECT * FROM FlowTemplate WHERE Id = @Id", new { Id = id }, transaction).ToArray();
//            return result.First();
//        }

//        public int Add(Core.FlowTemplate instance, IDbTransaction transaction = null)
//        {
//            if(_connection.State == ConnectionState.Closed) 
//                _connection.Open();

//            var id = _connection.Query<int>("SELECT TOP 1 Id FROM FlowTemplate ORDER BY Id DESC", null, transaction).First();
//            instance.Id = ++id;
//            _connection.Execute("INSERT INTO FlowTemplate (Id, Name) VALUES (@id, @name)", instance, transaction);
//            return _connection.Query<int>("SELECT TOP 1 Id FROM FlowTemplate ORDER BY Id DESC", null, transaction).First();

//        }

//        public void Update(int id, Core.FlowTemplate instance, IDbTransaction transaction = null)
//        {
//            if (_connection.State == ConnectionState.Closed)
//                _connection.Open();

//            // ensure row exists
//            Get(id, transaction);

//            _connection.Execute("UPDATE FlowTemplate SET Name=@name WHERE Id=@id", new { id, name = instance.Name }, transaction);
//        }

//        public void Delete(int id, IDbTransaction transaction = null)
//        {
//            if(_connection.State == ConnectionState.Closed)
//                _connection.Open();

//            _connection.Execute("DELETE FlowTemplate WHERE Id=@id", new {id}, transaction);
//        }
//    }
//}
