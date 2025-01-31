namespace Sentinel.Domain.DTO
{
    public class PapelDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IList<long> PermissaoIds { get; set; } = new List<long>();
    }
}
