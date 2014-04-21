using System;
using System.Collections.Generic;
using System.Linq;
using Flow.Library.Data.Abstract;
using Flow.Library.Validation;

namespace Flow.Library.Data.Repositories
{
    public class FlowTemplateStepRepository : IRepository<IFlowTemplateStep>
    {
        private readonly FlowDataContext _context;

        public FlowTemplateStepRepository(FlowDataContext context)
        {
            _context = context;
        }

        public IEnumerable<IFlowTemplateStep> Get()
        {
            return _context.FlowTemplateSteps.Select(o => new Core.FlowTemplateStep {Id = o.Id, Name = o.Name, FlowTemplateId = o.FlowTemplateId, StepTypeId = o.StepTypeId});
        }

        public IFlowTemplateStep Get(int id)
        {
            var items = _context.FlowTemplateSteps.Where(o => o.Id == id);
            if (!items.Any())
            {
                return null;
            }
            return items.Select(o => new Core.FlowTemplateStep {Id = o.Id, Name = o.Name, FlowTemplateId = o.FlowTemplateId, StepTypeId = o.StepTypeId}).First();
        }
        public void Add(IFlowTemplateStep item)
        {
            var data = new FlowTemplateStep
            {
                FlowTemplateId = item.FlowTemplateId,
                Name = item.Name,
                StepTypeId = item.StepTypeId
            };
            _context.FlowTemplateSteps.InsertOnSubmit(data);
        }

        public void Update(int id, IFlowTemplateStep instance)
        {
            var item = _context.FlowTemplateSteps.First(o => o.Id == id);
            if(item == null)
                throw new Exception("Item does not exist");

            if(instance.FlowTemplateId <= 0)
                throw new ValidationException("FlowTemplateId required");

            item.Name = instance.Name;
            item.FlowTemplateId = instance.FlowTemplateId;
        }

        public void Delete(int id)
        {
            _context.FlowTemplateSteps.DeleteAllOnSubmit(_context.FlowTemplateSteps.Where(o => o.Id == id));
        }

        public IEnumerable<IFlowTemplateStep> GetTemplateSteps(int id)
        {
            return
                _context.FlowTemplateSteps.Where(o => o.FlowTemplateId == id)
                    .Select(o => new Core.FlowTemplateStep {Id = o.Id, Name = o.Name, FlowTemplateId = id}).ToList();
        }
    }
}