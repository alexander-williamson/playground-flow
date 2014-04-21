using System;
using System.Data.SqlClient;
using System.Linq;
using Flow.Library.Data;
using Flow.Library.Data.Abstract;
using Flow.Library.Data.Repositories;
using Flow.Library.Steps;
using Xunit;
using FlowTemplateStep = Flow.Library.Core.FlowTemplateStep;

namespace Flow.Library.Tests.Data
{
    public class FlowTemplateStepRepositoryTests : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        private const string LocalConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\Sample Data\LocalDbTests.mdf"";Integrated Security=True";

        private readonly IRepository<IFlowTemplateStep> _repository;
        private readonly FlowDataContext _context;

        private Library.Data.FlowTemplate newTemplate;
        private Library.Data.FlowTemplateStep startStep, stopStep, collectDataStep, storeDataStep;

        public FlowTemplateStepRepositoryTests()
        {
            _connection = new SqlConnection(LocalConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _context = new FlowDataContext(_connection) { Transaction = _transaction };

            newTemplate = new FlowTemplate
            {
                Name = "Example Template 1"
            };

            _context.FlowTemplates.InsertOnSubmit(newTemplate);
            _context.SubmitChanges();

            startStep = new Library.Data.FlowTemplateStep
            {
                FlowTemplateId = newTemplate.Id,
                Name = "Example StartStep 1",
                StepTypeId = 1
            };

            collectDataStep = new Library.Data.FlowTemplateStep
            {
                FlowTemplateId = newTemplate.Id,
                Name = "Example CollectDataStep 2",
                StepTypeId = 3
            };

            storeDataStep = new Library.Data.FlowTemplateStep
            {
                FlowTemplateId = newTemplate.Id,
                Name = "Example StoreDataStep 3",
                StepTypeId = 4
            };

            stopStep = new Library.Data.FlowTemplateStep
            {
                FlowTemplateId = newTemplate.Id,
                Name = "Example StopStep 4",
                StepTypeId = 2
            };

            _context.FlowTemplateSteps.InsertAllOnSubmit(new[] { startStep, collectDataStep ,storeDataStep, stopStep });
            _context.SubmitChanges();
            _transaction.Save("insert");
            _repository = new FlowTemplateStepRepository(_context);
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

        ~FlowTemplateStepRepositoryTests()
        {
            Dispose(false);
        }

        public int GetLastId()
        {
            return _context.FlowTemplateSteps.Max(o => o.Id);
        }

        [Fact]
        public void Should_return_correct_amount_of_items_from_database()
        {
            var sut = _repository.Get().ToArray();

            Assert.Equal(4, sut.Count());
            Assert.Equal(collectDataStep.Id, sut[1].Id);
            Assert.Equal("Example CollectDataStep 2", sut[1].Name);
            Assert.Equal(newTemplate.Id, sut[1].FlowTemplateId);
        }

        [Fact]
        public void Should_return_template_steps()
        {
            var sut = _repository.Get(stopStep.Id);

            Assert.Equal(stopStep.Id, sut.Id);
            Assert.Equal("Example StopStep 4", sut.Name);
            Assert.Equal(newTemplate.Id, sut.FlowTemplateId);
        }

        [Fact]
        public void Should_return_null_if_template_step_does_not_exist()
        {
            Assert.Null(_repository.Get(-1));
        }

        [Fact]
        public void Should_update_row_with_new_data()
        {
            _repository.Update(collectDataStep.Id, new FlowTemplateStep { Name = "Updated", FlowTemplateId = 1 });

            var r = _context.FlowTemplateSteps;
            var sut = r.Where(o => o.Id == collectDataStep.Id).ToList().Last();

            Assert.Equal("Updated", sut.Name);
            Assert.Equal(1, sut.FlowTemplateId);
            Assert.Equal(collectDataStep.Id, sut.Id);
        }

        [Fact]
        public void Should_throw_exception_when_updating_if_row_does_not_exist()
        {
            Assert.Throws<InvalidOperationException>(() => _repository.Update(-1, new FlowTemplateStep { Name = "Updated " }));
        }

        [Fact]
        public void Should_remove_row_from_database_when_Step_deleted()
        {
            _repository.Delete(GetLastId());
            Assert.Equal(1, _context.FlowTemplates.Count());
        }

        [Fact]
        public void Should_return_correct_StartStep_for_single_step()
        {
            var result = _repository.Get(startStep.Id);
            Assert.Equal(1, result.StepTypeId);
        }

        [Fact]
        public void Should_return_correct_StopStep_for_single_step()
        {
            var result = _repository.Get(stopStep.Id);
            Assert.Equal(2, result.StepTypeId);
        }

        [Fact]
        public void Should_return_correct_CollectDataStep_for_single_step()
        {
            var result = _repository.Get(GetLastId());

            Assert.Equal(2, result.StepTypeId);
        }

        [Fact]
        public void Should_return_correct_StoreDataStep_for_single_step()
        {
            var result = _repository.Get(storeDataStep.Id);

            Assert.Equal(storeDataStep.Id, result.Id);
            Assert.Equal(4, result.StepTypeId);
        }

        [Fact]
        public void Should_return_correct_StartStep()
        {
            var result = _repository.Get().ToList();
            Assert.Equal(1, result[0].StepTypeId);
        }

        [Fact]
        public void Should_return_correct_StopStep()
        {
            var result = _repository.Get().ToList();
            Assert.Equal(3, result[1].StepTypeId);
        }

        [Fact]
        public void Should_return_correct_CollectDataStep()
        {
            var result = _repository.Get().ToList();
            Assert.Equal(4, result[2].StepTypeId);
        }

        [Fact]
        public void Should_return_correct_StoreDataStep()
        {
            var result = _repository.Get().ToList();
            Assert.Equal(2, result[3].StepTypeId);
        }
    }
}