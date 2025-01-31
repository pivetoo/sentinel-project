using Sentinel.Domain.Entities.Base;

namespace Sentinel.Domain.Entities
{
    public class Plano : EntidadeBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorMensal { get; set; }
        public ICollection<Contrato> Contratos { get; set; }
    }
}
