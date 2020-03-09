using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Translate.Repositories
{
    public interface IRepository<T> where T:class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        int GetCount();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        void AddRange(IEnumerable<T> entity);

        void Remove(T entity);
        void RemoveRage(IEnumerable<T> entity);
    }
}
