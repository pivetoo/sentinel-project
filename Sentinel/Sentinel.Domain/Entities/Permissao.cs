using Sentinel.Domain.Entities.Base;

namespace Sentinel.Domain.Entities
{
    public class Permissao : EntidadeBase
    {
        public string Recurso { get; set; }
        public string Acao { get; set; }
        public string Descricao { get; set; }
        public ICollection<Papel> Papeis { get; set; }
    }
}
