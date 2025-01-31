using Sentinel.Domain.Entities.Base;

namespace Sentinel.Domain.Entities
{
    public class Papel : EntidadeBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ICollection<Permissao> Permissoes { get; set; }
        public ICollection<UsuarioEmpresa> UsuariosEmpresa { get; set; }
    }
}
