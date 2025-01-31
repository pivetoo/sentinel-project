namespace Sentinel.Domain.DTO
{
    public class PermissaoDTO
    {
        public long Id { get; set; }
        public string Recurso { get; set; }
        public string Acao { get; set; }
        public string Descricao { get; set; }
    }
}
