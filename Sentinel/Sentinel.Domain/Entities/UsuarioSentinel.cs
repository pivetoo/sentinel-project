using Sentinel.Domain.Entities.Base;

namespace Sentinel.Domain.Entities
{
    public class UsuarioSentinel : EntidadeBase
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
