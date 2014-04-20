using System;
using System.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Repositories;
using Flow.Library.Validation;

namespace Flow.Library.Data
{
    // TODO Test Saving part of commit
    public class SqlUnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;
        private readonly FlowDataContext _context;

        public SqlUnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _context = new FlowDataContext(_connection);
            FlowTemplates = new FlowTemplateRepository(_context);
            FlowTemplateSteps = new FlowTemplateStepRepository(_context);
        }

        public IRepository<Core.FlowTemplate> FlowTemplates { get; set; }

        public IRepository<IFlowTemplateStep> FlowTemplateSteps { get; set; }

        public IRepository<IValidationRule> FlowTemplateStepRules { get; set; }

        public void Commit()
        {
            _context.SubmitChanges();
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
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}