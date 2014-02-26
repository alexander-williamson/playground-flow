using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Data.Repositories
{
    public class FlowTemplateSqlRepository : IFlowTemplateRepository
    {
        private const string ConnectionString = "Data Source=.;Initial Catalog=Flow;Integrated Security=True";

        public FlowTemplate GetTemplate(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplate>("SELECT * FROM FlowTemplate WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public IEnumerable<FlowTemplate> GetTemplates()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplate>("SELECT * FROM FlowTemplate");
            }
        }

        public FlowTemplateStep GetTemplateStep(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {

                return connection.Query<FlowTemplateStep>("SELECT * FROM FlowTemplateStep WHERE Id=@Id", new { Id = id }).First();
            }
        }

        public IEnumerable<FlowTemplateStep> GetTemplateStepsForTemplate(int templateId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplateStep>("SELECT * FROM FlowTemplateStep WHERE FlowTemplateId = @FlowTemplateId", new { FlowTemplateId = templateId });
            }
        }

        public FlowTemplateStepRule GetTemplateStepRule(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplateStepRule>("SELECT * FROM FlowTemplateStepRule WHERE FlowTemplateStepId = @FlowTemplateStepId", new { Id = id }).First();
            }
        }

        public IEnumerable<FlowTemplateStepRule> GetTemplateStepRulesForStep(int flowTemplateStepId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplateStepRule>("SELECT * FROM FlowTemplateStepRule WHERE FlowTemplateStepId = @FlowTemplateStepId", new { FlowTemplateStepId = flowTemplateStepId });
            }
        }
    }

}
