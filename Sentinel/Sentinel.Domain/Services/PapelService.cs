using Sentinel.Domain.Entities;
using Sentinel.Domain.Repositories;
using Sentinel.Domain.Services.Base;

namespace Sentinel.Domain.Services
{
    public class PapelService : CrudService<Papel>
    {
        private readonly IQueryRepository<Permissao> _permissaoRepository;

        public PapelService(ICrudRepository<Papel> repository, IQueryRepository<Permissao> permissaoRepository) : base(repository)
        {
            _permissaoRepository = permissaoRepository;
        }

        public void AtribuirPermissao(long papelId, long permissaoId)
        {
            var papel = GetById(papelId);
            var permissao = _permissaoRepository.GetById(permissaoId);

            if (permissao == null)
                throw new KeyNotFoundException("Permissão não encontrada.");

            if (papel.Permissoes.Any(p => p.Id == permissaoId))
                throw new InvalidOperationException("Papel já possui esta permissão.");

            papel.Permissoes.Add(permissao);
            Update(papel);
        }

        public void RemoverPermissao(long papelId, long permissaoId)
        {
            var papel = GetById(papelId);
            var permissao = papel.Permissoes.FirstOrDefault(p => p.Id == permissaoId);

            if (permissao == null)
                throw new InvalidOperationException("Papel não possui esta permissão.");

            papel.Permissoes.Remove(permissao);
            Update(papel);
        }

        #region Overrides
        public override void Add(Papel papel)
        {
            ValidarPapel(papel);
            base.Add(papel);
        }

        public override void Update(Papel papel)
        {
            var papelExistente = GetById(papel.Id);

            ValidarPapel(papel);

            papelExistente.Nome = papel.Nome;
            papelExistente.Descricao = papel.Descricao;

            base.Update(papelExistente);
        }
        #endregion

        #region Lógica Auxiliar
        private void ValidarPapel(Papel papel)
        {
            if (string.IsNullOrEmpty(papel.Nome))
                throw new InvalidOperationException("Nome do papel é obrigatório.");

            var nomeExistente = _repository.Query(p =>
                p.Nome == papel.Nome &&
                p.Id != papel.Id
            ).Any();

            if (nomeExistente)
                throw new InvalidOperationException("Já existe um papel com este nome.");
        }
        #endregion
    }
}
