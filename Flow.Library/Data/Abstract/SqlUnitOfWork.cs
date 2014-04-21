using System;
using System.Data;
using System.Data.Common;
using Flow.Library.Data.Repositories;
using Flow.Library.Validation;

namespace Flow.Library.Data.Abstract
{
    // TODO Test Saving part of commit
    public class SqlUnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;
        private readonly FlowDataContext _context;
        private readonly IDbTransaction _transaction;

        public SqlUnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _transaction = _connection.BeginTransaction();
            _context = new FlowDataContext(_connection) {Transaction = (DbTransaction) _transaction};
            FlowTemplates = new FlowTemplateRepository(_context);
            FlowTemplateSteps = new FlowTemplateStepRepository(_context);
        }

        public IRepository<Core.FlowTemplate> FlowTemplates { get; set; }

        public IRepository<IFlowTemplateStep> FlowTemplateSteps { get; set; }

        public IRepository<IValidationRule> FlowTemplateStepRules { get; set; }

        public void Commit()
        {
            _context.SubmitChanges();
            _transaction.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~SqlUnitOfWork()
        {
            Dispose(false);
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _transaction.Dispose();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}