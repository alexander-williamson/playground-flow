using System.Collections.Generic;
using Flow.Library.Data.Abstract;
using Flow.Library.Steps;

namespace Flow.Library.Data
{
    public interface IFlowTemplateService
    {
        IEnumerable<Core.FlowTemplate> GetFlowTemplates(IUnitOfWork unitOfWork);
        Core.FlowTemplate GetFlowTemplate(IUnitOfWork unitOfWork, int id);
        int Add(IUnitOfWork unitOfWork, Core.FlowTemplate template);
        void Update(IUnitOfWork unitOfWork, Core.FlowTemplate template);
        void Delete(IUnitOfWork unitOfWork, Core.FlowTemplate template);

        IEnumerable<IStep> GetFlowTemplateSteps(IUnitOfWork unitOfWork, int flowTemplateId);
        IStep GetFlowTemplateStep(IUnitOfWork unitOfWork, int id);
        int Add(IUnitOfWork unitOfWork, IStep step);
        void Update(IUnitOfWork unitOfWork, IStep step);
        void Delete(IUnitOfWork unitOfWork, IStep step);
    }
}