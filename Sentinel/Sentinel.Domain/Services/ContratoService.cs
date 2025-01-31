using Sentinel.Domain.DTO;
using Sentinel.Domain.Entities;
using Sentinel.Domain.Repositories;
using Sentinel.Domain.Services.Base;
using Sentinel.Domain.ValueObjects;

namespace Sentinel.Domain.Services
{
    public class ContratoService : CrudService<Contrato>
    {
        private readonly IQueryRepository<Empresa> _empresaRepository;
        private readonly IQueryRepository<Plano> _planoRepository;

        public ContratoService(
            ICrudRepository<Contrato> repository,
            IQueryRepository<Empresa> empresaRepository,
            IQueryRepository<Plano> planoRepository) : base(repository)
        {
            _empresaRepository = empresaRepository;
            _planoRepository = planoRepository;
        }

        public void CriarContrato(CriarContratoDto dto)
        {
            var empresa = _empresaRepository.GetById(dto.EmpresaId);
            if (empresa == null)
                throw new InvalidOperationException("Empresa não encontrada.");

            if (!empresa.Ativo)
                throw new InvalidOperationException("Empresa está inativa.");

            var plano = _planoRepository.GetById(dto.PlanoId);
            if (plano == null)
                throw new InvalidOperationException("Plano não encontrado.");

            var contrato = new Contrato
            {
                Empresa = empresa,
                Plano = plano,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                ValorFinal = dto.ValorFinal,
                Status = dto.Status != default ? dto.Status : StatusContrato.Ativo
            };

            base.Add(contrato);
        }


        public IEnumerable<Contrato> ObterPorEmpresa(long empresaId)
        {
            return GetByFilter(c => c.EmpresaId == empresaId);
        }

        public void AlterarStatus(long id, StatusContrato novoStatus)
        {
            var contrato = GetById(id);
            contrato.Status = novoStatus;
            Update(contrato);
        }

        #region Overrides
        public override void Add(Contrato contrato)
        {
            ValidarContrato(contrato);

            if (contrato.Status == default)
                contrato.Status = StatusContrato.Ativo;

            base.Add(contrato);
        }

        public override void Update(Contrato contrato)
        {
            var contratoExistente = GetById(contrato.Id);
            ValidarContrato(contrato);  

            contratoExistente.PlanoId = contrato.PlanoId;
            contratoExistente.ValorFinal = contrato.ValorFinal;
            contratoExistente.DataInicio = contrato.DataInicio;
            contratoExistente.DataFim = contrato.DataFim;
            contratoExistente.Status = contrato.Status;

            base.Update(contratoExistente);
        }
        #endregion

        #region Lógica Auxiliar
        private void ValidarContrato(Contrato contrato)
        {
            var empresa = _empresaRepository.GetById(contrato.EmpresaId);
            if (empresa == null)
                throw new InvalidOperationException("Empresa não encontrada.");

            if (!empresa.Ativo)
                throw new InvalidOperationException("Empresa está inativa.");

            var plano = _planoRepository.GetById(contrato.PlanoId);
            if (plano == null)
                throw new InvalidOperationException("Plano não encontrado.");

            if (contrato.DataInicio == default)
                throw new InvalidOperationException("Data de início é obrigatória.");

            if (contrato.DataFim.HasValue && contrato.DataFim.Value <= contrato.DataInicio)
                throw new InvalidOperationException("Data fim deve ser maior que data início.");

            if (contrato.ValorFinal <= 0)
                throw new InvalidOperationException("Valor deve ser maior que zero.");
        }
        #endregion
    }
}
