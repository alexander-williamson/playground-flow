using System.Collections.Generic;
using System.Linq;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Data
{
    public class FlowTemplateService
    {
        public static IEnumerable<Core.FlowTemplate> GetFlowTemplates(IUnitOfWork unitOfWork)
        {
            var results = unitOfWork.FlowTemplates.Get();
            return results;
        }

        public static Core.FlowTemplate GetFlowTemplate(IUnitOfWork unitOfWork, int id)
        {
            var result = unitOfWork.FlowTemplates.Get(id);
            return result;
        }

        public static int Add(IUnitOfWork unitOfWork, Core.FlowTemplate template)
        {
            var id = unitOfWork.FlowTemplates.Get().Any() ? unitOfWork.FlowTemplates.Get().Select(o => o.Id).Last() : 0;
            template.Id = ++id;
            unitOfWork.FlowTemplates.Add(template);
            unitOfWork.Commit();
            return id;
        }

        public static void Update(IUnitOfWork unitOfWork, Core.FlowTemplate template)
        {
            var item = unitOfWork.FlowTemplates.Get(template.Id);
            // update nodes etc.
            item.Name = template.Name;
            unitOfWork.Commit();
        }

        public static void Delete(IUnitOfWork unitOfWork, Core.FlowTemplate template)
        {
            unitOfWork.FlowTemplates.Delete(template.Id);
            unitOfWork.Commit();
        }
    }
}
