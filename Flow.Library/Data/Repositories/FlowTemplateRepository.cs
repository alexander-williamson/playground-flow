using System.Collections.Generic;
using System.Linq;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Data.Repositories
{
    public class FlowTemplateRepository : IRepository<Core.FlowTemplate>
    {
        private readonly FlowDataContext _context;

        public FlowTemplateRepository(FlowDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Core.FlowTemplate> Get()
        {
            return _context.FlowTemplates.Select(o => new Core.FlowTemplate { Id = o.Id, Name = o.Name });
        }
        public Core.FlowTemplate Get(int id)
        {
            var items = _context.FlowTemplates.Where(o => o.Id == id);
            return items.Any() ? items.Select(o => new Core.FlowTemplate {Id = o.Id, Name = o.Name}).First() : null;
        }
        public void Add(Core.FlowTemplate instance)
        {
            var newId = _context.FlowTemplates.Max(o => o.Id) + 1;
            _context.FlowTemplates.InsertOnSubmit(new FlowTemplate { Id = newId, Name = instance.Name });
            instance.Id = newId;
        }
        public void Update(int id, Core.FlowTemplate instance)
        {
            var existing = (from o in _context.FlowTemplates where o.Id == id select o).First();
            existing.Name = instance.Name;
        }
        public void Delete(int id)
        {
            var existing = (from o in _context.FlowTemplates where o.Id == id select o).First();
            _context.FlowTemplates.DeleteOnSubmit(existing);
        }

        public void Save()
        {
            _context.SubmitChanges();
        }
    }
}