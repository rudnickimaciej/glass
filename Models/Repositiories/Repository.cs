using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Translate.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }


        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }


        
        public int GetCount()
        {
            return _context.Set<T>().Count();
        }
        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }


        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }



        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRage(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);

        }
    }
}
