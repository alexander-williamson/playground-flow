using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Data.Repositories
{
    public class FlowTemplateSqlRepository : IFlowTemplateRepository
    {
        private readonly IDbConnection _dbConnection;

        public FlowTemplateSqlRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public FlowTemplate GetTemplate(int id)
        {
            _dbConnection.Open();
            return _dbConnection.Query<FlowTemplate>("SELECT * FROM FlowTemplate WHERE Id = @Id", new { Id = id }).First();
        }

        public IEnumerable<FlowTemplate> GetTemplates()
        {
            _dbConnection.Open();
            return _dbConnection.Query<FlowTemplate>("SELECT * FROM FlowTemplate");
        }

        public FlowTemplateStep GetTemplateStep(int id)
        {
            _dbConnection.Open();
            return _dbConnection.Query<FlowTemplateStep>("SELECT * FROM FlowTemplateStep WHERE Id=@Id", new { Id = id }).First();
        }

        public IEnumerable<FlowTemplateStep> GetTemplateStepsForTemplate(int templateId)
        {
            _dbConnection.Open();
            return _dbConnection.Query<FlowTemplateStep>("SELECT * FROM FlowTemplateStep WHERE FlowTemplateId = @FlowTemplateId", new { FlowTemplateId = templateId });
        }

        public FlowTemplateStepRule GetTemplateStepRule(int id)
        {
            _dbConnection.Open();
           return _dbConnection.Query<FlowTemplateStepRule>("SELECT * FROM FlowTemplateStepRule WHERE FlowTemplateStepId = @FlowTemplateStepId", new { Id = id }).First();
        }

        public IEnumerable<FlowTemplateStepRule> GetTemplateStepRulesForStep(int flowTemplateStepId)
        {
            _dbConnection.Open();
            return _dbConnection.Query<FlowTemplateStepRule>("SELECT * FROM FlowTemplateStepRule WHERE FlowTemplateStepId = @FlowTemplateStepId", new { FlowTemplateStepId = flowTemplateStepId });
        }
    }

}
