using System;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using Flow.Library.Data;
using Flow.Library.Data.Repositories;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;

namespace Flow.Library.Tests.Data
{
    public class FlowTemplateRepositoryTests : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        private const string LocalConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\Sample Data\LocalDbTests.mdf"";Integrated Security=True";

        private readonly FlowTemplateRepository _repository;
        private readonly FlowDataContext _context;

        public FlowTemplateRepositoryTests()
        {
            _connection = new SqlConnection(LocalConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            using (var command = new SqlCommand(@"INSERT INTO FlowTemplate (Name) VALUES ('Example Template 1');", _connection, _transaction))
            {
                command.ExecuteNonQuery();
            }
            _transaction.Save("insert");
            _context = new FlowDataContext(_connection) {Transaction = _transaction};
            _repository = new FlowTemplateRepository(_context);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction.Rollback();
                _connection.Close();
            }
        }

        ~FlowTemplateRepositoryTests()
        {
            Dispose(false);
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
            var lastInsertedId = GetLastId();
            var sut = _repository.Get(lastInsertedId);

            Assert.Equal(lastInsertedId, sut.Id);
            Assert.Equal("Example Template 1", sut.Name);
        }

        private int GetLastId()
        {
            return _context.FlowTemplates.Max(o => o.Id);
        }

        [Fact]
        public void Should_return_null_if_template_not_exists()
        {
            Assert.Null(_repository.Get(-1));
        }

        [Fact]
        public void Should_add_item_to_context()
        {
            var expectedCount = _context.FlowTemplates.Count() + 1;
            var instance = new FlowTemplate {Name = "Example Template 2"};

            _repository.Add(instance);
            _context.SubmitChanges();

            Assert.Equal(expectedCount, _context.FlowTemplates.Count());
        }

        [Fact]
        public void Should_update_row_with_new_data()
        {
            _repository.Update(GetLastId(), new FlowTemplate {Id = 2, Name = "Updated"});

            var sut = _context.FlowTemplates.First();

            Assert.Equal("Updated", sut.Name);
            Assert.Equal(GetLastId(), sut.Id);
        }

        [Fact]
        public void Should_throw_exception_when_updating_if_row_does_not_exist()
        {
            Assert.Throws<InvalidOperationException>(() => _repository.Update(-1, new FlowTemplate {Name = "Updated "}));
        }

        [Fact]
        public void Should_remove_row_from_database()
        {
            _repository.Delete(GetLastId());
            _context.SubmitChanges();
            Assert.Equal(0, _context.FlowTemplates.Count());
        }
    }
}
