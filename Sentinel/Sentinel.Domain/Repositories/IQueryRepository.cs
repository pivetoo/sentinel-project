using System.Linq.Expressions;

namespace Sentinel.Domain.Repositories
{
    public interface IQueryRepository<T> where T : class
    {
        T GetById(long id);
        IQueryable<T> Query();
        IQueryable<T> Query(Expression<Func<T, bool>> where);
    }
}
