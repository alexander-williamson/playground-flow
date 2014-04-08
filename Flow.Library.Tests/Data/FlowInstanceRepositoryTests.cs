using System;
using System.Data.SqlClient;
using System.Linq;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Repositories;
using Xunit;
using FlowInstance = Flow.Library.Core.FlowInstance;

namespace Flow.Library.Tests.Data
{
    public class FlowInstanceRepositoryTests : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        private const string LocalConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\Sample Data\LocalDbTests.mdf"";Integrated Security=True";
        
        private readonly IRepository<FlowInstance> _flowInstanceRepository;
        private readonly FlowDataContext _context;

        public FlowInstanceRepositoryTests()
        {
            Console.WriteLine("SomeFixture ctor: This should only be run once");
            _connection = new SqlConnection(LocalConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            using (var command = new SqlCommand("INSERT INTO FlowInstance (Id) VALUES (1);", _connection, _transaction))
            {
                command.ExecuteNonQuery();
            }
            _transaction.Save("insert");
            _context = new FlowDataContext(_connection);
            _context.Transaction = _transaction;
            _flowInstanceRepository = new FlowInstanceRepository(_context);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _transaction.Rollback();
                _connection.Close();
            }
        }

        ~FlowInstanceRepositoryTests()
        {
            Dispose(false);
        }

        [Fact]
        public void Should_return_flow_from_database()
        {
            // assemble
            var sut = _flowInstanceRepository.Get(1);

            // assert
            Assert.Equal(1, sut.Id);
            Assert.Equal(0, sut.CompletedSteps.Count);
            Assert.Equal(0, sut.Variables.Count);
        }

        [Fact]
        public void Should_insert_flow_into_database()
        {
            _flowInstanceRepository.Add(new FlowInstance());
            _flowInstanceRepository.Save();

            Assert.Equal(2, _context.FlowInstances.Count());
        }

        [Fact]
        public void Should_throw_exception_if_cannot_find_item()
        {
            Assert.Throws<InvalidOperationException>(() => _flowInstanceRepository.Get(20));
        }

    }
}
