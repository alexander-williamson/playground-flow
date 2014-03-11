using System;
using System.Data.SqlClient;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Repositories;
using Xunit;

namespace Flow.Library.Tests.Data
{
    public class FlowInstanceRepositoryTests : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        private const string LocalConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\Sample Data\LocalDbTests.mdf"";Integrated Security=True";
        
        private readonly IFlowInstanceRepository _flowInstanceRepository;

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
            _flowInstanceRepository = new FlowInstanceRepository(_connection, _transaction);
        }

        public void Dispose()
        {
            _transaction.Rollback();
            _connection.Close();
        }

        [Fact]
        public void Should_return_flow_from_database()
        {
            // assemble
            var sut = _flowInstanceRepository.Get(1, _transaction);

            // assert
            Assert.Equal(1, sut.Id);
            Assert.Equal(0, sut.CompletedSteps.Count);
            Assert.Equal(0, sut.Variables.Count);
        }

        [Fact]
        public void Should_insert_flow_into_database()
        {
            var sut = _flowInstanceRepository.Add(new FlowInstance(), _transaction);

            Assert.Equal(2, sut);
        }

        [Fact]
        public void Should_throw_exception_if_cannot_find_item()
        {
            Assert.Throws<InvalidOperationException>(() => _flowInstanceRepository.Get(20));
        }

    }
}
