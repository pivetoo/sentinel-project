using Sentinel.Domain.Entities.Base;

namespace Sentinel.Domain.Entities
{
    public class Empresa : EntidadeBase
    {
        public string Nome { get; set; }
        public string TenantId { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Contrato> Contratos { get; set; }
        public ICollection<UsuarioEmpresa> Usuarios { get; set; }
    }
}
