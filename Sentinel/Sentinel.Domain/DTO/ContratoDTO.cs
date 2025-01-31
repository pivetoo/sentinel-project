using Sentinel.Domain.ValueObjects;

namespace Sentinel.Domain.DTO
{
    public class CriarContratoDto
    {
        public long EmpresaId { get; set; }
        public long PlanoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal ValorFinal { get; set; }
        public StatusContrato Status { get; set; }
    }
}
