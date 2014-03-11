using System;
using System.Data.SqlClient;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Repositories;
using Xunit;

namespace Flow.Library.Tests.Data
{
    public class FlowTemplateRepositoryTests : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        private const string LocalConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\Sample Data\LocalDbTests.mdf"";Integrated Security=True";
        
        private readonly IFlowTemplateRepository _repository;

        public FlowTemplateRepositoryTests()
        {
            _connection = new SqlConnection(LocalConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            using (var command = new SqlCommand(@"INSERT INTO FlowTemplate (Id, Name) VALUES (1, 'Example Template 1');", _connection, _transaction))
            {
                command.ExecuteNonQuery();
            }
            _transaction.Save("insert");
            _repository = new FlowTemplateSqlRepository(_connection, _transaction);
        }

        public void Dispose()
        {
            _transaction.Rollback();
            _connection.Close();
        }

        [Fact]
        public void Should_return_template()
        {
            var sut = _repository.Get(1);

            Assert.Equal(1, sut.Id);
            Assert.Equal("Example Template 1", sut.Name);
        }

        [Fact]
        public void Should_thrown_exception_if_template_not_exists()
        {
            Assert.Throws<InvalidOperationException>(() => _repository.Get(-1));
        }

        [Fact]
        public void Should_return_id_when_inserting_template()
        {
            var sut = _repository.Add(new FlowTemplate {Name = "Example Template 2"});
            Assert.Equal(2, sut);
        }
    }
}
