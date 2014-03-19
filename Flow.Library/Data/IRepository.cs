using System.Collections.Generic;
using System.Data;

namespace Flow.Library.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(IDbTransaction transaction = null);
        T Get(int id, IDbTransaction transaction = null);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void Save(IDbTransaction transaction = null);
    }
}
