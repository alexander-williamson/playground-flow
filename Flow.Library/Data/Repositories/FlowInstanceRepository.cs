using System;
using System.Collections.Generic;
using System.Linq;
using Flow.Library.Data.Abstract;

namespace Flow.Library.Data.Repositories
{
    public class FlowInstanceRepository : IRepository<Core.FlowInstance>
    {
        private readonly FlowDataContext _context;
        public FlowInstanceRepository(FlowDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Core.FlowInstance> Get()
        {
            return _context.FlowInstances.Select(o => new Core.FlowInstance { Id = o.Id });
        }

        public Core.FlowInstance Get(int id)
        {
            return _context.FlowInstances.Where(o => o.Id == id).Select(o => new Core.FlowInstance {Id = o.Id}).First();
        }

        public void Add(Core.FlowInstance item)
        {
            _context.FlowInstances.InsertOnSubmit(new FlowInstance {Id = item.Id});
        }

        public void Update(int id, Core.FlowInstance instance)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var item =  _context.FlowInstances.First(o => o.Id == id);
            _context.FlowInstances.DeleteOnSubmit(item);
        }

        public void Save()
        {
            _context.SubmitChanges();
        }
    }
}
