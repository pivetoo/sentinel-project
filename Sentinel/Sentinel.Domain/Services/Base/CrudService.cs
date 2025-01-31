using Sentinel.Domain.Entities.Base;
using Sentinel.Domain.Repositories;
using System.Linq.Expressions;

namespace Sentinel.Domain.Services.Base
{
    public class CrudService<T> where T : EntidadeBase
    {
        protected readonly ICrudRepository<T> _repository;

        public CrudService(ICrudRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual void Add(T entity)
        {
            var currentDate = DateTime.UtcNow;
            entity.CriadoEm = entity.CriadoEm == default ? currentDate : entity.CriadoEm;
            entity.UltimaAlteracao = currentDate;

            _repository.Add(entity);
        }

        public virtual T GetById(long id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"{typeof(T).Name} not found with ID {id}.");
            }
            return entity;
        }

        public virtual void Update(T entity)
        {
            entity.UltimaAlteracao = DateTime.UtcNow;
            _repository.Update(entity);
        }

        public virtual void Delete(long id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"{typeof(T).Name} not found with ID {id}.");
            }
            _repository.Delete(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.Query().ToList();
        }

        public virtual IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            return _repository.Query(filter).ToList();
        }
    }
}
