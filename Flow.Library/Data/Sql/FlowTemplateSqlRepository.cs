using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Flow.Library.Data.Models;

namespace Flow.Library.Data.Sql
{
    public class FlowTemplateSqlRepository : IFlowTemplateRepository
    {
        private const string ConnectionString = "Data Source=.;Initial Catalog=Flow;Integrated Security=True";

        public FlowTemplateDataModel GetFlowTemplate(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplateDataModel>("SELECT * FROM FlowTemplateDataModel WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public IEnumerable<FlowTemplateDataModel> GetFlowTemplates()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplateDataModel>("SELECT * FROM FlowTemplateDataModel");
            }
        }

        public FlowTemplateStepDataModel GetFlowTemplateStep(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {

                return connection.Query<FlowTemplateStepDataModel>("SELECT * FROM FlowTemplateStep WHERE Id=@Id", new { Id = id }).First();
            }
        }

        public IEnumerable<FlowTemplateStepDataModel> GetFlowTemplateStepsForTemplate(int templateId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplateStepDataModel>("SELECT * FROM FlowTemplateStep WHERE FlowTemplateId = @FlowTemplateId", new { FlowTemplateId = templateId });
            }
        }

        public FlowTemplateStepRuleDataModel GetFlowTemplateStepRule(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplateStepRuleDataModel>("SELECT * FROM FlowTemplateStepRule WHERE FlowTemplateStepId = @FlowTemplateStepId", new { Id = id }).First();
            }
        }

        public IEnumerable<FlowTemplateStepRuleDataModel> GetFlowTemplateStepRulesForStep(int flowTemplateStepId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<FlowTemplateStepRuleDataModel>("SELECT * FROM FlowTemplateStepRule WHERE FlowTemplateStepId = @FlowTemplateStepId", new { FlowTemplateStepId = flowTemplateStepId });
            }
        }
    }

}
