using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Flow.Library.Data
{
    public class FlowTemplateRepository : IRepository<Core.FlowTemplate>
    {
        private readonly FlowDatabaseClassesDataContext _context;

        public FlowTemplateRepository(FlowDatabaseClassesDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Core.FlowTemplate> Get(IDbTransaction transaction = null)
        {
            return _context.FlowTemplates.Select(o => new Core.FlowTemplate { Id = o.Id, Name = o.Name });
        }
        public Core.FlowTemplate Get(int id, IDbTransaction transaction = null)
        {
            return _context.FlowTemplates.Where(o => o.Id == id).Select(o => new Core.FlowTemplate { Id = o.Id, Name = o.Name }).First();
        }
        public void Add(Core.FlowTemplate instance, IDbTransaction transaction = null)
        {
            _context.FlowTemplates.InsertOnSubmit(new FlowTemplate { Id = instance.Id, Name = instance.Name });
        }
        public void Update(int id, Core.FlowTemplate instance, IDbTransaction transaction = null)
        {
            var existing = (from o in _context.FlowTemplates where o.Id == id select o).First();
            existing.Name = instance.Name;
        }
        public void Delete(int id, IDbTransaction transaction = null)
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