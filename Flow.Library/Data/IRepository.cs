using System.Collections.Generic;
using System.Data;

namespace Flow.Library.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(IDbTransaction transaction = null);
        T Get(int id, IDbTransaction transaction = null);
        void Add(T instance, IDbTransaction transaction = null);
        void Update(int id, T instance, IDbTransaction transaction = null);
        void Delete(int id, IDbTransaction transaction = null);
        void Save();
    }
}
