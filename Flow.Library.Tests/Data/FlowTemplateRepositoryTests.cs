using System;
using System.Data.SqlClient;
using System.Linq;
using Flow.Library.Data;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Flow.Library.Tests.Data
{
    public class FlowTemplateRepositoryTests : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        private const string LocalConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\Sample Data\LocalDbTests.mdf"";Integrated Security=True";
        
        private readonly IRepository<FlowTemplate> _repository;
        private readonly FlowDatabaseClassesDataContext _context;

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
            _context = new FlowDatabaseClassesDataContext(_connection);
            _context.Transaction = _transaction;
            _repository = new FlowTemplateRepository(_context);
        }

        public void Dispose()
        {
            _transaction.Rollback();
            _connection.Close();
        }

        [Fact]
        public void Should_return_correct_amount_of_items_from_database()
        {
            var sut = _repository.Get();

            Assert.Equal(1, sut.Count());
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
            _repository.Add(new FlowTemplate {Name = "Example Template 2"});
            _repository.Save();
            Assert.Equal(2, _context.FlowTemplates.Count());
        }

        [Fact]
        public void Should_update_row_with_new_data()
        {
            _repository.Update(1, new FlowTemplate {Id = 2, Name = "Updated"});
            _repository.Save();

            var sut = _context.FlowTemplates.First();

            Assert.Equal("Updated", sut.Name);
            Assert.Equal(1, sut.Id);
        }

        [Fact]
        public void Should_throw_exception_when_updating_if_row_does_not_exist()
        {
            Assert.Throws<InvalidOperationException>(() => _repository.Update(-1, new FlowTemplate {Name = "Updated "}));
        }

        [Fact]
        public void Should_remove_row_from_database()
        {
            _repository.Delete(1);
            _repository.Save();
            Assert.Equal(0, _context.FlowTemplates.Count());
        }
    }
}
