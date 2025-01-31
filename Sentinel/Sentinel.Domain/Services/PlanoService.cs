using Sentinel.Domain.Entities;
using Sentinel.Domain.Repositories;
using Sentinel.Domain.Services.Base;
using Sentinel.Domain.ValueObjects;

namespace Sentinel.Domain.Services
{
    public class PlanoService : CrudService<Plano>
    {
        public PlanoService(ICrudRepository<Plano> repository) : base(repository)
        {
        }

        public IEnumerable<Plano> ObterPlanosAtivos()
        {
            return GetByFilter(p => p.Contratos.Any(c => c.Status == StatusContrato.Ativo));
        }

        #region Overrides
        public override void Add(Plano plano)
        {
            ValidarPlano(plano);

            if (plano.Contratos == null)
                plano.Contratos = new List<Contrato>();

            base.Add(plano);
        }

        public override void Update(Plano plano)
        {
            var planoExistente = GetById(plano.Id);
            ValidarPlano(plano);

            planoExistente.Nome = plano.Nome;
            planoExistente.Descricao = plano.Descricao;
            planoExistente.ValorMensal = plano.ValorMensal;

            base.Update(planoExistente);
        }
        #endregion

        #region Lógica Auxiliar
        private void ValidarPlano(Plano plano)
        {
            if (string.IsNullOrEmpty(plano.Nome))
                throw new InvalidOperationException("Nome é obrigatório.");

            if (plano.ValorMensal <= 0)
                throw new InvalidOperationException("Valor mensal deve ser maior que zero.");

            var nomeExistente = _repository.Query(p =>
                p.Nome == plano.Nome &&
                p.Id != plano.Id
            ).Any();

            if (nomeExistente)
                throw new InvalidOperationException("Já existe um plano com este nome.");
        }
        #endregion
    }
}
