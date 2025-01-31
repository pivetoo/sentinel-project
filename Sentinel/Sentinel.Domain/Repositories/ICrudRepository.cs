using Sentinel.Domain.Entities.Base;

namespace Sentinel.Domain.Repositories
{
    public interface ICrudRepository<T> : IQueryRepository<T> where T : EntidadeBase
    {
        void Add(T entity);
        void Delete(T entity);
        T Update(T entity);
        void Upsert(T entity);
    }
}
