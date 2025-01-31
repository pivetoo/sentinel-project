using Sentinel.Domain.Entities.Base;
using Sentinel.Domain.ValueObjects;

namespace Sentinel.Domain.Entities
{
    public class Contrato : EntidadeBase
    {
        public long EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public long PlanoId { get; set; }
        public Plano Plano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal ValorFinal { get; set; }
        public StatusContrato Status { get; set; }
    }
}
