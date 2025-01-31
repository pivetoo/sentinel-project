using Sentinel.Domain.Entities;
using Sentinel.Domain.Repositories;
using Sentinel.Domain.Services.Base;

namespace Sentinel.Domain.Services
{
    public class PermissaoService : CrudService<Permissao>
    {
        public PermissaoService(ICrudRepository<Permissao> repository) : base(repository)
        {
        }

        public IEnumerable<Permissao> ObterPorRecurso(string recurso)
        {
            return GetByFilter(p => p.Recurso == recurso);
        }

        public override void Add(Permissao permissao)
        {
            ValidarPermissao(permissao);
            base.Add(permissao);
        }

        public override void Update(Permissao permissao)
        {
            var permissaoExistente = GetById(permissao.Id);

            ValidarPermissao(permissao);

            permissaoExistente.Recurso = permissao.Recurso;
            permissaoExistente.Acao = permissao.Acao;
            permissaoExistente.Descricao = permissao.Descricao;

            base.Update(permissaoExistente);
        }

        private void ValidarPermissao(Permissao permissao)
        {
            if (string.IsNullOrEmpty(permissao.Recurso))
                throw new InvalidOperationException("Recurso é obrigatório.");

            if (string.IsNullOrEmpty(permissao.Acao))
                throw new InvalidOperationException("Ação é obrigatória.");

            var permissaoExistente = _repository.Query(p =>
                p.Recurso == permissao.Recurso &&
                p.Acao == permissao.Acao &&
                p.Id != permissao.Id
            ).Any();

            if (permissaoExistente)
                throw new InvalidOperationException("Já existe uma permissão com este recurso e ação.");
        }
    }
}
