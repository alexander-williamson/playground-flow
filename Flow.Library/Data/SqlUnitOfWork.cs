using System;
using System.Data;
using System.Data.SqlClient;
using Flow.Library.Core;
using Flow.Library.Data.Repositories;

namespace Flow.Library.Data
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly FlowTemplateSqlRepository _flowinTemplateSqlRepository;
        private readonly IDbConnection _connection;
        
        public SqlUnitOfWork()
        {
            _connection = new SqlConnection("");
            _flowinTemplateSqlRepository = new FlowTemplateSqlRepository(_connection);
        }

        ~SqlUnitOfWork()
        {
            _connection.Dispose();
        }

        public IRepository<FlowTemplate> FlowInstances
        {
            get
            {
                return _flowinTemplateSqlRepository;
            }
        }

        public IRepository<FlowTemplateStep> FlowTemplateSteps
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public IRepository<FlowTemplateStepRule> FlowTemplateStepRules
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public void Commit()
        {
            using (var transaction = _connection.BeginTransaction())
            {
                _flowinTemplateSqlRepository.Save(transaction);
            }
        }
    }
}