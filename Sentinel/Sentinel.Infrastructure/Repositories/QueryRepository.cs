using Microsoft.EntityFrameworkCore;
using Sentinel.Domain.Repositories;
using Sentinel.Infrastructure.Data;
using System.Linq.Expressions;

namespace Sentinel.Infrastructure.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public QueryRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where);
        }
    }
}
