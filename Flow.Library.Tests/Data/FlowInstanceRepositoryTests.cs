using System;
using System.Data.SqlClient;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Repositories;
using Xunit;

namespace Flow.Library.Tests.Data
{
    public class HelperFixture : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        private const string LocalConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\Sample Data\LocalDbTests.mdf"";Integrated Security=True";

        public SqlTransaction Transaction
        {
            get { return _transaction; }
        }

        public SqlConnection Connection
        {
            get { return _connection; }
        }

        public HelperFixture()
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
        }

        public void Dispose()
        {
            Console.WriteLine("SomeFixture: Disposing SomeFixture");
            _transaction.Rollback();
            _connection.Close();
        }
    }


    public class FlowInstanceRepositoryTests : IUseFixture<HelperFixture>, IDisposable
    {
        private IFlowInstanceRepository _flowInstanceRepository;
        private SqlTransaction _transaction;
        private SqlConnection _connection;

        public void SetFixture(HelperFixture data)
        {
            _transaction = data.Transaction;
            _connection = data.Connection;
            _flowInstanceRepository = new FlowInstanceRepository(_connection, _transaction);
        }

        public FlowInstanceRepositoryTests()
        {
            // required for xunit
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing");
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
            var sut = _flowInstanceRepository.Add(new FlowInstance());

            Assert.Equal(2, sut);
        }

    }
}
