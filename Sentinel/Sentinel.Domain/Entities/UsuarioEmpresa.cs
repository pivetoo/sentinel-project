using Sentinel.Domain.Entities.Base;

namespace Sentinel.Domain.Entities
{
    public class UsuarioEmpresa : EntidadeBase
    {
        public long EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Papel> Papeis { get; set; }
    }
}
