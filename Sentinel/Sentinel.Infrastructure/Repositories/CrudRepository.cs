using Microsoft.EntityFrameworkCore;
using Sentinel.Domain.Entities.Base;
using Sentinel.Domain.Repositories;
using Sentinel.Infrastructure.Data;

namespace Sentinel.Infrastructure.Repositories
{
    public class CrudRepository<T> : QueryRepository<T>, ICrudRepository<T> where T : EntidadeBase
    {
        public CrudRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(T entidade)
        {
            _dbSet.Remove(entidade);
            _context.SaveChanges();
        }

        public void Add(T entidade)
        {
            var dataAtual = DateTime.UtcNow;
            if (entidade.CriadoEm == default(DateTime))
            {
                entidade.CriadoEm = dataAtual;
            }
            if (entidade.UltimaAlteracao == default(DateTime))
            {
                entidade.UltimaAlteracao = dataAtual;
            }

            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public T Update(T entidade)
        {
            entidade.UltimaAlteracao = DateTime.UtcNow;
            _dbSet.Update(entidade);
            _context.SaveChanges();
            return entidade;
        }

        public void Upsert(T entidade)
        {
            if (entidade.Id == 0)
            {
                entidade.CriadoEm = DateTime.UtcNow;
                entidade.UltimaAlteracao = entidade.CriadoEm;
                _dbSet.Add(entidade);
            }
            else
            {
                entidade.UltimaAlteracao = DateTime.UtcNow;
                _dbSet.Update(entidade);
            }
            _context.SaveChanges();
        }
    }
}
