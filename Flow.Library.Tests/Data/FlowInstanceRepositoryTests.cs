using System.Data.SqlClient;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Repositories;
using Xunit;

namespace Flow.Library.Tests.Data
{
    public class FlowInstanceRepositoryTests
    {
        private readonly IFlowInstanceRepository _flowInstanceRepository;

        private const string LocalConnectionString =
            @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\Sample Data\LocalDbTests.mdf"";Integrated Security=True";

        private readonly SqlConnection _connection = new SqlConnection(LocalConnectionString);
        private readonly SqlTransaction _transaction;
        
        public FlowInstanceRepositoryTests()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _flowInstanceRepository = new FlowInstanceRepository(_connection, _transaction);
        }

        ~FlowInstanceRepositoryTests()
        {
            _transaction.Rollback();
            _connection.Close();
        }

        [Fact]
        public void Should_return_flow_from_database()
        {
            // assemble
            var sut = _flowInstanceRepository.GetFlow(1);

            // assert
            Assert.Equal(1, sut.Id);
            Assert.Equal(0, sut.CompletedSteps.Count);
            Assert.Equal(0, sut.Variables.Count);
        }

    }
}
