using System.Collections.Generic;
using System.Data;
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
            return _context.FlowTemplates.Where(o => o.Id == id).Select(o => new Core.FlowTemplate { Id = o.Id, Name = o.Name }).First();
        }
        public void Add(Core.FlowTemplate instance)
        {
            _context.FlowTemplates.InsertOnSubmit(new FlowTemplate { Id = instance.Id, Name = instance.Name });
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